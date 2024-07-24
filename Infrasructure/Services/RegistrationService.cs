using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Extension;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using MimeKit.Text;
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
		private readonly ILoginService _loginService;

		public RegistrationService(UserManager<User> userManager, IOptions<HostUrlOptions> hostUrlOptions, IEmailService emailService, ILoginService loginService)
		{
			_userManager = userManager;
			_hostUrlOptions = hostUrlOptions.Value;
			_emailService = emailService;
			_loginService = loginService;
		}

		public async Task<bool> ConfirmEmail(string userId, string token, string email = null, CancellationToken cancellationToken = default)
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

			if (!string.IsNullOrEmpty(email))
			{
				var decodeEmail = HtmlUtils.HtmlDecode(email);

				if (!decodeEmail.Contains('@'))
				{
					throw new LogicException("Invalid email.");
				}

				user.Email = decodeEmail;

				var emailResult = await _userManager.UpdateAsync(user);

				if (!emailResult.Succeeded)
				{
					throw new LogicException(emailResult.GetIdentityErrorText());
				}

				await _loginService.Logout(cancellationToken);
				await _userManager.UpdateSecurityStampAsync(user);
			}

			var result = await _userManager.ConfirmEmailAsync(user, token);

			if (!result.Succeeded)
			{
				throw new LogicException(result.GetIdentityErrorText());
			}

			return true;
		}

		public async Task SendConfirmation(string userId, string email = null)
		{
			var user = await _userManager.FindByIdAsync(userId);

			await SendConfirmation(user, email);
		}

		public async Task SendConfirmation(User user, string email = null)
		{
			var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

			var encodedToken = HttpUtility.UrlEncode(token);

			var callbackUrl = $"{_hostUrlOptions.Url}/confirmRegistration?userId={user.Id}&token={encodedToken}";

			var title = "Confirm registration";

			var to = user.Email;

			if (!string.IsNullOrEmpty(email))
			{
				title = "Confirm change Email";
				callbackUrl += $"&email={HttpUtility.UrlEncode(email)}";
				to = email;
			}

			await _emailService.Send(to, title, $"For confirm your email. Follow: <a href='{callbackUrl}'>link</a>");
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
				throw new LogicException(result.GetIdentityErrorText());
			}

			var roleResult = await _userManager.AddToRoleAsync(user, Role.UserRole);

			if (!roleResult.Succeeded)
			{
				throw new LogicException(result.GetIdentityErrorText());
			}

			await SendConfirmation(user);

			return user;
		}
	}
}
