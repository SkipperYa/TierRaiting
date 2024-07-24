using AutoMapper;
using Domain.Models;
using Infrastructure.Commands;
using Infrastructure.Commands.RegistrationUser.Create;
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

		/// <summary>
		/// Create new User in system
		/// </summary>
		/// <param name="command">Class with Email, UserName, Password and PasswordConfirm</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="UserViewModel"/></returns>
		[HttpPost]
		public async Task<IActionResult> Registration([FromBody] RegistrationUserCommand command, CancellationToken cancellationToken)
		{
			var user = await _mediator.Send(command, cancellationToken);

			var userViewModel = _mapper.Map<UserViewModel>(user);

			return Ok(userViewModel);
		}

		/// <summary>
		/// Send confirmation email after successful registration to user email address 
		/// </summary>
		/// <param name="command">Class contains UserId and Email</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return true if email has been send otherwise throw exception with message</returns>
		[HttpPut]
		public async Task<IActionResult> SendConfirmation([FromBody] SendConfirmCommand command, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(command, cancellationToken);

			return Ok(result);
		}

		/// <summary>
		/// Handle token from confirmation email and try confirm user email
		/// </summary>
		/// <param name="userId">UserId</param>
		/// <param name="token">Token from confirmation email</param>
		/// <param name="email">Confirmation email</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return true if email has been confirmed otherwise throw exception with message</returns>
		[HttpGet]
		public async Task<IActionResult> ConfirmRegistration(string userId, string token, string email, CancellationToken cancellationToken)
		{
			var command = new ConfirmUserCommand()
			{
				Token = token,
				UserId = userId,
				Email = email,
			};

			var result = await _mediator.Send(command, cancellationToken);

			return Ok(result);
		}
	}
}
