using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Registration
{
	public class RegistrationController : BaseApplicationController
	{
		private readonly IMapper _mapper;
		private readonly IRegistrationService _registrationService;

		public RegistrationController(IMediator mediator, IMapper mapper, IRegistrationService registrationService) : base(mediator)
		{
			_mapper = mapper;
			_registrationService = registrationService;
		}

		[HttpPost]
		public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command, CancellationToken cancellationToken)
		{
			var user = await _mediator.Send(command, cancellationToken);

			var userViewModel = _mapper.Map<UserViewModel>(user);

			return Ok(userViewModel);
		}

		[HttpPut]
		public async Task<IActionResult> SendConfirmation([FromBody] string userId, CancellationToken cancellationToken)
		{
			await _registrationService.SendConfirmation(userId);

			return Ok("ok");
		}

		[HttpGet]
		public async Task<IActionResult> ConfirmRegistration(string userId, string token, CancellationToken cancellationToken)
		{
			var result = await _registrationService.ConfirmEmail(userId, token);

			return Ok(result);
		}
	}
}
