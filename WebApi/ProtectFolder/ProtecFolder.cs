using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WebApi
{
	public class ProtectFolderOptions
	{
		public IAuthorizationRequirement Requirement { get; set; }
	}

	public class ProtectFolder
	{
		private readonly RequestDelegate _next;
		private readonly IAuthorizationRequirement _requirement;

		public ProtectFolder(RequestDelegate next, ProtectFolderOptions options)
		{
			_next = next;
			_requirement = options.Requirement;
		}

		public async Task Invoke(HttpContext httpContext, IAuthorizationService authorizationService)
		{
			// If user not sign in and try get /images return 401
			if (httpContext.Request.Path.StartsWithSegments("/images"))
			{
				var authorized = await authorizationService.AuthorizeAsync(httpContext.User, null, _requirement);

				if (!authorized.Succeeded)
				{
					await httpContext.ChallengeAsync();
					return;
				}
			}

			await _next(httpContext);
		}
	}
}
