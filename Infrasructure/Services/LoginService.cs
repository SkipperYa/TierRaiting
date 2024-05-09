using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<User> _signInManager;
		private readonly ApplicationContext _context;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public LoginService(SignInManager<User> signInManager, ApplicationContext context, IHttpContextAccessor httpContextAccessor)
		{
			_signInManager = signInManager;
			_context = context;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<User> Login(string email, string password, CancellationToken cancellationToken)
		{
			var user = await _context.Set<User>()
				.AsNoTracking()
				.Where(q => q.Email == email)
				.FirstOrDefaultAsync(cancellationToken);

			if (user is null)
			{
				throw new LogicException("Invalid user");
			}

			if (!await _signInManager.UserManager.IsEmailConfirmedAsync(user))
			{
				return user;
			}

			var result = await _signInManager.PasswordSignInAsync(user, password, true, false);

			if (!result.Succeeded)
			{
				throw new LogicException("Invalid password or login");
			}

			return user;
		}

		public async Task Logout(CancellationToken cancellationToken)
		{
			await _signInManager.SignOutAsync();
		}
	}
}
