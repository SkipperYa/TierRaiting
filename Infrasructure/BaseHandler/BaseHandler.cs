using Infrastructure.BaseRequest;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure
{
	public abstract class BaseHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
		where TRequest : BaseRequest<TResult>
	{
		public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
	}

	public abstract class BaseAuthorizeHandler<TRequest, TResult> : IRequestHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
	{
		public abstract Task<TResult> Handle(TRequest request, CancellationToken cancellationToken);
	}
}
