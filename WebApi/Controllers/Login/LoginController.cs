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

		/// <summary>
		/// Login User in to system by Email and Password
		/// </summary>
		/// <param name="model">Class contains Email and Password</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="ProfileViewModel"/> user otherwise throw exception with message</returns>
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

		/// <summary>
		/// Logout user
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> if user logout otherwise throw exception with message</returns>
		[HttpGet]
		public async Task<IActionResult> Logout(CancellationToken cancellationToken)
		{
			await _mediator.Send(new LogoutCommand(), cancellationToken);

			return Ok(new { Message = "Ok" });
		}
	}
}
