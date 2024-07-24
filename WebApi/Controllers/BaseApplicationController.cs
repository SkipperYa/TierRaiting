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
	public abstract class BaseApplicationController : ControllerBase
	{
		protected readonly IMediator _mediator;

		/// <summary>
		/// Get UserId if user sign in otherwise return empty Guid
		/// </summary>
		protected Guid UserId
		{
			get => IsAuthenticated && Guid.TryParse(User.FindFirst(ClaimTypes.NameIdentifier).Value, out var guid)
				? guid
				: Guid.Empty;
		}

		protected bool IsAuthenticated
		{
			get => User.Identity.IsAuthenticated;
		}

		public BaseApplicationController(IMediator mediator)
		{
			_mediator = mediator;
		}
	}
}
