using Domain.Entities;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	public class SteamAppController : BaseApplicationController
	{
		public SteamAppController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetSteamApps(string text, CancellationToken cancellationToken)
		{
			var query = new GetSteamAppQuery()
			{
				Text = text,
			};

			var steamApps = await _mediator.Send(query, cancellationToken);

			return Ok(new
			{
				List = steamApps
			});
		}
	}
}
