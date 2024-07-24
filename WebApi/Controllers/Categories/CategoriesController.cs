using Domain.Entities;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class CategoriesController : BaseApplicationController
	{
		public CategoriesController(IMediator mediator) : base(mediator)
		{

		}

		/// <summary>
		/// Get a list of user <see cref="Category">Categories</see>
		/// </summary>
		/// <param name="page">Page, by default 1</param>
		/// <param name="text">Text search by <see cref="Category.Title" /></param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="PagedList{TEntity}">PagedList</see> of <see cref="Category">Categories</see></returns>
		[HttpGet]
		public async Task<IActionResult> GetCategories(int page = 1, string text = null, CancellationToken cancellationToken = default)
		{
			var categories = await _mediator.Send(new CategoriesQuery()
			{
				Page = page,
				Text = text
			}, cancellationToken);

			return Ok(categories);
		}
	}
}
