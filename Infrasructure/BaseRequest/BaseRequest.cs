using MediatR;

namespace Infrastructure.BaseRequest
{
	public abstract class BaseRequest<TResult> : IRequest<TResult>
	{
	}

	public abstract class BaseAuthorizeRequest<TResult> : BaseRequest<TResult>
	{
		public string UserId { get; set; }
	}

	public abstract class BaseAuthorizeListRequest<TResult> : BaseAuthorizeRequest<TResult>
	{
		public int Page { get; set; }
		public int Count { get; set; } = 10;
		public string Text { get; set; }
	}
}
