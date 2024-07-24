using AutoMapper;
using Domain.Entities;
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
	public class CategoryController : BaseApplicationController
	{
		private readonly IMapper _mapper;

		public CategoryController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}

		/// <summary>
		/// Get <see cref="Category" /> by Id
		/// </summary>
		/// <param name="id">Category Id</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="CategoryViewModel">Category</see></returns>
		[HttpGet]
		public async Task<IActionResult> GetCategory(Guid id, CancellationToken cancellationToken)
		{
			var query = new GetCategoryQuery
			{
				Id = id,
			};

			var categoryViewModel = await _mediator.Send(query, cancellationToken);

			return Ok(categoryViewModel);
		}

		/// <summary>
		/// Create <see cref="Category" />
		/// </summary>
		/// <param name="command"><see cref="CreateCategoryCommand" /> contains fields for new <see cref="Category" /></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with created <see cref="CategoryViewModel">Category</see></returns>
		[HttpPost]
		public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command, CancellationToken cancellationToken)
		{
			var category = await _mediator.Send(command, cancellationToken);

			var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

			return Ok(categoryViewModel);
		}

		/// <summary>
		/// Update <see cref="Category" /> by Id
		/// </summary>
		/// <param name="command"><see cref="UpdateCategoryCommand" /> contains fields for exists <see cref="Category" /></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with updated <see cref="CategoryViewModel">Category</see></returns>
		[HttpPut]
		public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand command, CancellationToken cancellationToken)
		{
			var category = await _mediator.Send(command, cancellationToken);

			var categoryViewModel = _mapper.Map<CategoryViewModel>(category);

			return Ok(categoryViewModel);
		}

		/// <summary>
		/// Delete <see cref="Category" /> by Id
		/// </summary>
		/// <param name="command"><see cref="DeleteCategoryCommand" /> include Id for delete entity</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Deleted entity</returns>
		[HttpDelete]
		public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand command, CancellationToken cancellationToken)
		{
			var deleted = await _mediator.Send(command, cancellationToken);

			return Ok(deleted);
		}
	}
}
