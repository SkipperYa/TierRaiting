using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Commands.RegistrationUser.Create;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands.Profile
{
	public class UpdateUserHandler : BaseAuthorizeHandler<UpdateUserCommand, User>
	{
		protected readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;

		public UpdateUserHandler(IMediator mediator, UserManager<User> userManager)
		{
			_mediator = mediator;
			_userManager = userManager;
		}

		public override async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());

			if (user is null)
			{
				throw new LogicException("Invalid user");
			}

			var emailIsChanged = user.Email != request.Email;
			user.Email = request.Email;
			user.UserName = request.UserName;
			user.Src = request.Src;

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				if (emailIsChanged)
				{
					await _mediator.Send(new SendConfirmCommand()
					{
						UserId = user.Id.ToString()
					}, cancellationToken);
				}

				return user;
			}
			else
			{
				var stringBuilder = new StringBuilder();

				foreach (var error in result.Errors)
				{
					stringBuilder.Append(error.Description);
				}

				throw new LogicException(stringBuilder.ToString());
			}
		}
	}
}
