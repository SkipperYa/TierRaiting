using Domain.Entities;
using Infrastructure.Commands.Profile.BanUser;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Admin.Ban
{
	[Authorize]
	[Authorize(Roles = Role.AdminRole)]
	public class BanController : BaseApplicationController
	{
		public BanController(IMediator mediator) : base(mediator)
		{
		}

		/// <summary>
		/// Set <see cref="User.LockoutEnabled" /> to !LockoutEnabled and <see cref="User.LockoutEnd" /> to DateTime.MaxValue
		/// </summary>
		/// <param name="id">User Id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>True if user has been banned, false otherwise</returns>
		[HttpGet]
		public async Task<IActionResult> Ban(string id, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new BanUserCommand()
			{
				Id = Guid.TryParse(id, out var userId) ? userId : Guid.Empty,
			}, cancellationToken);

			return Ok(result);
		}
	}
}
