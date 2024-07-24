using Domain.Entities;
using Domain.Enum;
using Domain.Models;
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

		/// <summary>
		/// Get a list of <see cref="ItemViewModel" />
		/// </summary>
		/// <param name="categoryId">CategoryId</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="PagedList{TEntity}">PagedList</see> of <see cref="ItemViewModel">ItemViewModel</see></returns>
		[HttpGet]
		public async Task<IActionResult> GetItems(Guid categoryId, CancellationToken cancellationToken)
		{
			var items = await _mediator.Send(new GetItemsQuery()
			{
				CategoryId = categoryId,
				Page = 0,
				Ordering = Ordering.Ascending,
			}, cancellationToken);

			return Ok(items);
		}
	}
}
