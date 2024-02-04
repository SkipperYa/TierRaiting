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
				UserId = UserId
			};

			var ItemViewModel = await _mediator.Send(query, cancellationToken);

			return Ok(ItemViewModel);
		}

		[HttpPost]
		public async Task<IActionResult> CreateItem([FromBody] CreateItemCommand command, CancellationToken cancellationToken)
		{
			command.UserId = UserId;

			var Item = await _mediator.Send(command, cancellationToken);

			var ItemViewModel = _mapper.Map<ItemViewModel>(Item);

			return Ok(ItemViewModel);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateItem([FromBody] UpdateItemCommand command, CancellationToken cancellationToken)
		{
			command.UserId = UserId;

			var Item = await _mediator.Send(command, cancellationToken);

			var ItemViewModel = _mapper.Map<ItemViewModel>(Item);

			return Ok(ItemViewModel);
		}

		[HttpDelete]
		public async Task<IActionResult> DeleteItem([FromBody] DeleteItemCommand command, CancellationToken cancellationToken)
		{
			command.UserId = UserId;

			var deleted = await _mediator.Send(command, cancellationToken);

			return Ok(deleted);
		}
	}
}
