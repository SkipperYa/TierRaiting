using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Registration
{
	public class RegistrationController : BaseApplicationController
	{
		private readonly IRegistrationService _registrationService;
		private readonly ILoginService _loginService;

		public RegistrationController(IMediator mediator, IRegistrationService registrationService, ILoginService loginService) : base(mediator)
		{
			_registrationService = registrationService;
			_loginService = loginService;
		}

		[HttpPost]
		public async Task<IActionResult> Registration([FromForm] RegistrationViewModel model, CancellationToken cancellationToken)
		{
			var user = await _registrationService.Registration(model, cancellationToken);

			var jwtToken = _loginService.GetToken(user);

			return Ok(new
			{
				Token = jwtToken,
				User = user
			});
		}
	}
}
