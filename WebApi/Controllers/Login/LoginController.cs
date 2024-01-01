using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Login
{
	public class LoginController : BaseApplicationController
	{
		private readonly ILoginService _loginService;

		public LoginController(IMediator mediator, ILoginService loginService) : base(mediator)
		{
			_loginService = loginService;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromForm] LoginViewModel model, CancellationToken cancellationToken)
		{
			var user = await _loginService.Login(model, cancellationToken);

			var token = _loginService.GetToken(user);

			return Ok(new
			{
				Token = token,
				User = user
			});
		}

		[HttpGet]
		public async Task<IActionResult> Logout(CancellationToken cancellationToken)
		{
			await _loginService.Logout(cancellationToken);

			return Redirect("/");
		}
	}
}
