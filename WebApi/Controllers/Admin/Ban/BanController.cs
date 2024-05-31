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
