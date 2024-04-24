using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Infrastructure.Services
{
	public class RegistrationService : IRegistrationService
	{
		private readonly UserManager<User> _userManager;
		private readonly HostUrlOptions _hostUrlOptions;
		private readonly IEmailService _emailService;

		public RegistrationService(UserManager<User> userManager, IOptions<HostUrlOptions> hostUrlOptions, IEmailService emailService)
		{
			_userManager = userManager;
			_hostUrlOptions = hostUrlOptions.Value;
			_emailService = emailService;
		}

		public async Task<bool> ConfirmEmail(string userId, string token)
		{
			if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
			{
				throw new LogicException("Invalid data.");
			}

			var user = await _userManager.FindByIdAsync(userId);

			if (user is null)
			{
				throw new LogicException("Invalid user");
			}

			var result = await _userManager.ConfirmEmailAsync(user, token);

			if (!result.Succeeded)
			{
				var stringBuilder = new StringBuilder();

				foreach (var error in result.Errors)
				{
					stringBuilder.Append(error.Description);
				}

				throw new LogicException(stringBuilder.ToString());
			}

			return true;
		}

		public async Task SendConfirmation(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);

			await SendConfirmation(user);
		}

		public async Task SendConfirmation(User user)
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			var encodedToken = HttpUtility.UrlEncode(token);

			var callbackUrl = $"{_hostUrlOptions.Url}/confirmRegistration?userId={user.Id}&token={encodedToken}";

			await _emailService.Send(user.Email, "Confirm registration", $"For confirm your registration follow: <a href='{callbackUrl}'>link</a>");
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

			await SendConfirmation(user);

			return user;
		}
	}
}
