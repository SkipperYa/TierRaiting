using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands.Profile.BanUser
{
	public class BanUserCommandHandler : BaseAuthorizeHandler<BanUserCommand, bool>
	{
		private readonly UserManager<User> _userManager;

		public BanUserCommandHandler(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public override async Task<bool> Handle(BanUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _userManager.FindByIdAsync(request.Id.ToString());

			if (user is null || await _userManager.IsInRoleAsync(user, Role.AdminRole))
			{
				throw new LogicException("Invalid user");
			}

			user.LockoutEnabled = !user.LockoutEnabled;
			user.LockoutEnd = !user.LockoutEnabled ? DateTime.MaxValue : null;

			var updateResult = await _userManager.UpdateAsync(user);

			if (updateResult.Succeeded)
			{
				var securityResult = await _userManager.UpdateSecurityStampAsync(user);

				if (!securityResult.Succeeded)
				{
					var stringBuilder = new StringBuilder();

					foreach (var error in updateResult.Errors)
					{
						stringBuilder.Append(error.Description);
					}

					throw new LogicException(stringBuilder.ToString());
				}
			}
			else
			{
				var stringBuilder = new StringBuilder();

				foreach (var error in updateResult.Errors)
				{
					stringBuilder.Append(error.Description);
				}

				throw new LogicException(stringBuilder.ToString());
			}

			return user.LockoutEnabled;
		}
	}
}
