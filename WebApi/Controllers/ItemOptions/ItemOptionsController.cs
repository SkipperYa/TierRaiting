using Domain.Entities;
using Domain.Enum;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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

		/// <summary>
		/// Get list of <see cref="ItemOption" /> options by <see cref="CategoryType" /> and text
		/// </summary>
		/// <param name="text">Title of Game, Books or Films</param>
		/// <param name="categoryType"><see cref="CategoryType" /></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="List{TEntity}">List</see> of <see cref="ItemOption">Categories</see></returns>
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
