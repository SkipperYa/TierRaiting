using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BaseApplicationController : ControllerBase
	{
		protected readonly IMediator _mediator;

		public BaseApplicationController(IMediator mediator)
		{
			_mediator = mediator;
		}
	}
}
