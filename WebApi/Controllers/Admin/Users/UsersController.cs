using Domain.Entities;
using Domain.Models;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Threading.Tasks;

namespace WebApi.Controllers.Admin.Users
{
	[Authorize(Roles = Role.AdminRole)]
	public class UsersController : BaseApplicationController
	{
		public UsersController(IMediator mediator) : base(mediator)
		{
		}

		public async Task<IActionResult> GetUsers(int page = 1, string text = null, CancellationChangeToken cancellationChangeToken = default)
		{
			return Ok(new PagedList<UserViewModel>());
		}
	}
}
