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
