using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Category
{
	[Authorize]
	public class CategoryController : BaseApplicationController
	{
		private readonly IMapper _mapper;

		public CategoryController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
		{
			command.UserId = UserId;

			var category = await _mediator.Send(command, cancellationToken);

			var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

			return Ok(categoryViewModel);
		}
	}
}
