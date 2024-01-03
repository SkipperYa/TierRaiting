using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.Commands.RegistrationUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Registration
{
	public class RegistrationController : BaseApplicationController
	{
		private readonly ILoginService _loginService;
		private readonly IMapper _mapper;

		public RegistrationController(IMediator mediator, ILoginService loginService, IMapper mapper) : base(mediator)
		{
			_loginService = loginService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command, CancellationToken cancellationToken)
		{
			var user = await _mediator.Send(command, cancellationToken);

			var jwtToken = _loginService.GetToken(user);

			var userViewModel = _mapper.Map<UserViewModel>(user);

			return Ok(new
			{
				Token = jwtToken,
				User = userViewModel
			});
		}
	}
}
