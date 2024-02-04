using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using System;
using System.Threading;
using System.Threading.Tasks;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Infrastructure.Utils;
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

		public async Task<User> Login(LoginViewModel model, CancellationToken cancellationToken)
		{
			var user = await _context.Set<User>()
				.AsNoTracking()
				.Where(q => q.Email == model.Email)
				.Select(q => new User() 
				{
					Id = q.Id,
					UserName = q.UserName
				})
				.FirstOrDefaultAsync();

			if (user is null)
			{
				throw new LogicException("Invalid user");
			}

			var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);

			if (!result.Succeeded)
			{
				throw new LogicException("Invalid password or login");
			}

			var token = GetToken(user);

			_httpContextAccessor.HttpContext.Response.Cookies.Append(AuthOptions.TOKENNAME, token, new CookieOptions()
			{
				HttpOnly = true
			});

			return user;
		}

		public string GetToken(User user)
		{
			var now = DateTime.UtcNow;

			var claims = new List<Claim> 
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				new Claim(ClaimTypes.Name, user.UserName),
			};

			var jwt = new JwtSecurityToken(
				issuer: AuthOptions.ISSUER,
				audience: AuthOptions.AUDIENCE,
				notBefore: now,
				expires: now.AddDays(365),
				claims: claims,
				signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
			);

			var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

			return encodedJwt;
		}

		public async Task Logout(CancellationToken cancellationToken)
		{
			await _signInManager.SignOutAsync();

			_httpContextAccessor.HttpContext.Response.Cookies.Delete("access_token");
		}
	}
}
