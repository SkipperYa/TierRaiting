using MediatR;
using Microsoft.AspNetCore.Http.Timeouts;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace WebApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[RequestTimeout("DefaultTimeout10s")]
	public class BaseApplicationController : ControllerBase
	{
		protected readonly IMediator _mediator;

		protected Guid UserId
		{
			get => User.Identity.IsAuthenticated 
				? Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value)
				: Guid.Empty;
		}

		public BaseApplicationController(IMediator mediator)
		{
			_mediator = mediator;
		}
	}
}
