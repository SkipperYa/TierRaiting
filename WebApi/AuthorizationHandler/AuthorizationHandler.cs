using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace WebApi.AuthorizationHandler
{
	public class ProtectFolderAuthorizationRequirement : IAuthorizationRequirement
	{

	}

	public class ProtectFolderAuthorizationHandler : AuthorizationHandler<ProtectFolderAuthorizationRequirement>
	{
		protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ProtectFolderAuthorizationRequirement requirement)
		{
			if (!context.User.Identity.IsAuthenticated)
			{
				context.Fail();
			}
			else
			{
				context.Succeed(requirement);
			}

			return Task.CompletedTask;
		}
	}
}
