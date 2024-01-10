using Domain.Interfaces;
using MediatR;

namespace Infrastructure.BaseRequest
{
	public abstract class BaseRequest<TResult> : IRequest<TResult>
	{
	}

	public interface IBaseAuthorizeRequest : IWithId
	{
		public string UserId { get; set; }
	}

	public abstract class BaseAuthorizeRequest<TResult> : BaseRequest<TResult>, IBaseAuthorizeRequest
	{
		public string UserId { get; set; }
		public string Id { get; set; }
	}

	public interface IBaseGetAuthorizeRequest : IBaseAuthorizeRequest
	{
		public string Id { get; set; }
	}

	public abstract class BaseGetAuthorizeRequest<TResult> : BaseAuthorizeRequest<TResult>, IBaseGetAuthorizeRequest
	{
		public string Id { get; set; }
	}

	public interface IBaseAuthorizeListRequest
	{
		public int Page { get; set; }
		public int Count { get; set; }
		public string Text { get; set; }
	}

	public abstract class BaseAuthorizeListRequest<TResult> : BaseAuthorizeRequest<TResult>, IBaseAuthorizeListRequest
	{
		public int Page { get; set; } = 1;
		public int Count { get; set; } = 10;
		public string Text { get; set; }
	}
}
