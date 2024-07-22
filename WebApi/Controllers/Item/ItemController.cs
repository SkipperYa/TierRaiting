using AutoMapper;
using Domain.Models;
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

		[HttpPost]
		public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
		{
			var item = await _mediator.Send(command, cancellationToken);

			var itemViewModel = _mapper.Map<ItemViewModel>(item);

			return Ok(itemViewModel);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateItem([FromBody] UpdateItemCommand command, CancellationToken cancellationToken)
		{
			var item = await _mediator.Send(command, cancellationToken);

			var itemViewModel = _mapper.Map<ItemViewModel>(item);

			return Ok(itemViewModel);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteItem([FromBody] DeleteItemCommand command, CancellationToken cancellationToken)
		{
			var deleted = await _mediator.Send(command, cancellationToken);

			return Ok(deleted);
		}
	}
}
