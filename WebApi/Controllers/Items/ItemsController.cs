using Domain.Enum;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class ItemsController : BaseApplicationController
	{
		public ItemsController(IMediator mediator) : base(mediator)
		{

		}

		[HttpGet]
		public async Task<IActionResult> GetItems(Guid categoryId, CancellationToken cancellationToken)
		{
			var items = await _mediator.Send(new GetItemsQuery()
			{
				CategoryId = categoryId,
				UserId = UserId,
				Page = 0,
				Ordering = Ordering.Ascending,
			}, cancellationToken);

			return Ok(items);
		}
	}
}
