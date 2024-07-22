using Domain.Models;
using Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Login
{
	public class LoginController : BaseApplicationController
	{
		public LoginController(IMediator mediator) : base(mediator)
		{
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginViewModel model, CancellationToken cancellationToken)
		{
			var userViewModel = await _mediator.Send(new LoginCommand()
			{
				Email = model.Email,
				Password = model.Password
			}, cancellationToken);

			return Ok(userViewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Logout(CancellationToken cancellationToken)
		{
			await _mediator.Send(new LogoutCommand(), cancellationToken);

			return Ok(new { Message = "Ok" });
		}
	}
}
