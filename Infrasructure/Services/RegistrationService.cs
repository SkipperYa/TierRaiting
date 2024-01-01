using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class RegistrationService : IRegistrationService
	{
		private readonly UserManager<User> _userManager;

		public RegistrationService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}

		public async Task<User> Registration(RegistrationViewModel model, CancellationToken cancellationToken)
		{
			var user = new User
			{
				UserName = model.Email,
				Email = model.Email,
			};

			var result = await _userManager.CreateAsync(user, model.Password);

			if (!result.Succeeded)
			{
				var stringBuilder = new StringBuilder();

				foreach (var error in result.Errors)
				{
					stringBuilder.Append(error.Description);
				}

				throw new LogicException(stringBuilder.ToString());
			}

			return user;
		}
	}
}
