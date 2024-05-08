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
