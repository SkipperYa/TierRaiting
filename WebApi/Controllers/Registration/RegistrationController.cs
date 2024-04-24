using AutoMapper;
using Domain.Models;
using Infrastructure.Commands.RegistrationUser.Create;
using Infrastructure.Services;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Registration
{
    public class RegistrationController : BaseApplicationController
	{
		private readonly IMapper _mapper;

		public RegistrationController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command, CancellationToken cancellationToken)
		{
			var user = await _mediator.Send(command, cancellationToken);

			var userViewModel = _mapper.Map<UserViewModel>(user);

			return Ok(userViewModel);
		}

		[HttpPut]
		public async Task<IActionResult> ConfirmRegistration(string userId, string token, CancellationToken cancellationToken)
		{
			var result = await RegistrationService.ConfirmEmail(userId, token);

			return Ok(userViewModel);
		}
	}
}
