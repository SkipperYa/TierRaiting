using AutoMapper;
using Domain.Models;
using Domain.Entities;
using Infrastructure.Commands;
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
	public class ItemController : BaseApplicationController
	{
		private readonly IMapper _mapper;

		public ItemController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}

		/// <summary>
		/// Get <see cref="Item"/> by Id
		/// </summary>
		/// <param name="id">Item Id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="ItemViewModel">Item</see></returns>
		[HttpGet]
		public async Task<IActionResult> GetItem(Guid id, CancellationToken cancellationToken)
		{
			var query = new GetItemQuery
			{
				Id = id,
			};

			var itemViewModel = await _mediator.Send(query, cancellationToken);

			return Ok(itemViewModel);
		}

		/// <summary>
		/// Create <see cref="Item" />
		/// </summary>
		/// <param name="command"><see cref="CreateItemCommand" /> contains fields for new <see cref="Item" /></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with created <see cref="ItemViewModel">Item</see></returns>
		[HttpPost]
		public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
		{
			var item = await _mediator.Send(command, cancellationToken);

			var itemViewModel = _mapper.Map<ItemViewModel>(item);

			return Ok(itemViewModel);
		}

		/// <summary>
		/// Update <see cref="Item" /> by Id
		/// </summary>
		/// <param name="command"><see cref="UpdateItemCommand" /> contains fields for exists <see cref="Item" /></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with updated <see cref="ItemViewModel">Item</see></returns>
		[HttpPut]
		public async Task<IActionResult> UpdateItem([FromBody] UpdateItemCommand command, CancellationToken cancellationToken)
		{
			var item = await _mediator.Send(command, cancellationToken);

			var itemViewModel = _mapper.Map<ItemViewModel>(item);

			return Ok(itemViewModel);
		}

		/// <summary>
		/// Delete <see cref="Item" /> by Id
		/// </summary>
		/// <param name="command"><see cref="DeleteItemCommand" /> include Id for delete entity</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Deleted entity</returns>
		[HttpDelete]
		public async Task<IActionResult> DeleteItem([FromBody] DeleteItemCommand command, CancellationToken cancellationToken)
		{
			var deleted = await _mediator.Send(command, cancellationToken);

			return Ok(deleted);
		}
	}
}
