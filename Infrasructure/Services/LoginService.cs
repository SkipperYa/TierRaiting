using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Serilog;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Infrastructure.Utils;

namespace Infrastructure.Services
{
	public class LoginService : ILoginService
	{
		private readonly SignInManager<User> _signInManager;
		private readonly ApplicationContext _context;

		public LoginService(SignInManager<User> signInManager, ApplicationContext context)
		{
			_signInManager = signInManager;
			_context = context;
		}

		public async Task<User> Login(LoginViewModel model, CancellationToken cancellationToken)
		{
			var user = await _context.Set<User>()
				.AsNoTracking()
				.FirstOrDefaultAsync(q => q.Email == model.Email);

			if (user is null)
			{
				throw new LogicException("Invalid user;");
			}

			var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

			if (!result.Succeeded)
			{
				throw new LogicException("Invalid password or login");
			}

			return user;
		}

		public string GetToken(User user)
		{
			var now = DateTime.UtcNow;

			var claims = new List<Claim> { new Claim(ClaimTypes.NameIdentifier, user.Id) };

			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.ISSUER,
				audience: AuthOptions.AUDIENCE,
				notBefore: now,
				claims: claims,
				signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
			);

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return encodedJwt;
		}

		public async Task Logout(CancellationToken cancellationToken)
		{
			await _signInManager.SignOutAsync();
		}
	}
}
