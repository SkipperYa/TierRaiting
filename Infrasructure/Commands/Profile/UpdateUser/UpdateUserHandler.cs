using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Commands.RegistrationUser.Create;
using Infrastructure.Database;
using Infrastructure.Extension;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands.Profile
{
	public class UpdateUserHandler : BaseAuthorizeHandler<UpdateUserCommand, User>
	{
		private readonly IMediator _mediator;
		private readonly UserManager<User> _userManager;
		private readonly ApplicationContext _applicationContext;

		public UpdateUserHandler(IMediator mediator, UserManager<User> userManager, ApplicationContext applicationContext)
		{
			_mediator = mediator;
			_userManager = userManager;
			_applicationContext = applicationContext;
		}

		public override async Task<User> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.UserId.ToString());

			if (user is null)
			{
				throw new LogicException("Invalid user");
			}

			var emailIsChanged = user.Email != request.Email;

			if (emailIsChanged && !await _userManager.IsEmailConfirmedAsync(user))
			{
				throw new LogicException("Current email is not confirmed");
			}

			user.UserName = request.UserName;
			user.Src = request.Src;

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				if (emailIsChanged)
				{
					await _mediator.Send(new SendConfirmCommand()
					{
						UserId = user.Id.ToString(),
						Email = request.Email,
					}, cancellationToken);
				}

				return user;
			}

			throw new LogicException(result.GetIdentityErrorText());
		}
	}
}
