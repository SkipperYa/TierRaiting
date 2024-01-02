using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class BaseApplicationController : ControllerBase
	{
		protected readonly IMediator _mediator;

		protected string UserId
		{
			get => User.Identity.IsAuthenticated 
				? User.FindFirst(ClaimTypes.NameIdentifier).Value
				: null;
		}

		public BaseApplicationController(IMediator mediator)
		{
			_mediator = mediator;
		}
	}
}
