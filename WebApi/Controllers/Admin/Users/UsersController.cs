using Domain.Entities;
using Domain.Enum;
using Infrastructure.Database;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Admin.Users
{
	[Authorize]
	[Authorize(Roles = Role.AdminRole)]
	public class UsersController : BaseApplicationController
	{
		public UsersController(IMediator mediator) : base(mediator)
		{
		}

		/// <summary>
		/// Get a list of application users, only for <see cref="Role.AdminRole" />
		/// </summary>
		/// <param name="page">Page, by default 1</param>
		/// <param name="text">Text search by <see cref="User.Email" /> or <see cref="User.UserName" /></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="PagedList{TEntity}">PagedList</see> of <see cref="User">Users</see></returns>
		[HttpGet]
		public async Task<IActionResult> GetUsers(int page = 1, string text = null, CancellationToken cancellationToken = default)
		{
			var users = await _mediator.Send(new UsersQuery()
			{
				Text = text,
				Page = page,
				Ordering = Ordering.Ascending,
			}, cancellationToken);

			return Ok(users);
		}
	}
}
