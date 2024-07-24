﻿using Domain.Enum;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class ItemOptionsController : BaseApplicationController
	{
		public ItemOptionsController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet]
		public async Task<IActionResult> GetSteamApps(string text, CategoryType categoryType, CancellationToken cancellationToken)
		{
			var query = new GetItemOptionsQuery()
			{
				Text = text,
				CategoryType = categoryType,
			};

			var options = await _mediator.Send(query, cancellationToken);

			return Ok(new
			{
				List = options
			});
		}
	}
}
