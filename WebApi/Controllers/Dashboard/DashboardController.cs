using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebApi.Controllers.Dashboard
{
	[Authorize]
	public class DashboardController : BaseApplicationController
	{
		public DashboardController(IMediator mediator) : base(mediator)
		{
		}

		/// <summary>
		/// Get User Dashboard statistics
		/// </summary>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="DashboardResult" /></returns>
		[HttpGet]
		public async Task<IActionResult> GetDashboard()
		{
			var result = await _mediator.Send(new GetDashboardQuery());

			return Ok(result);
		}
	}
}
