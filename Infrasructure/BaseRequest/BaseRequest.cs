using MediatR;

namespace Infrastructure.BaseRequest
{
	public abstract class BaseRequest<TResult> : IRequest<TResult>
	{
	}

	public abstract class BaseAuthorizeRequest<TResult> : BaseRequest<TResult>
	{
		public string UserId { get; set; }

		public BaseAuthorizeRequest(string userId)
		{
			UserId = userId;
		}
	}
}
