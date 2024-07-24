using Infrastructure.BaseRequest;
using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Behaviors
{
	public class AuthenticationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest
		: IRequest<TResponse>
	{
		private readonly IHttpContextAccessor _contextAccessor;

		public AuthenticationBehavior(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}

		public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
		{
			// Set UserId to Request if TRequest is IBaseAuthorizeRequest from Claims
			if (request is IBaseAuthorizeRequest authorizeRequest)
			{
				var user = _contextAccessor.HttpContext.User;

				authorizeRequest.UserId = user.Identity.IsAuthenticated && Guid.TryParse(user.FindFirst(ClaimTypes.NameIdentifier).Value, out var guid)
					? guid
					: Guid.Empty;
			}

			return next();
		}
	}
}
