using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
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

		public async Task<User> Registration(string email, string userName, string password, CancellationToken cancellationToken)
		{
			var user = new User
			{
				UserName = userName,
				Email = email,
			};

			var result = await _userManager.CreateAsync(user, password);

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
