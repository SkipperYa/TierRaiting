using Domain.Enum;
using Domain.Interfaces;
using MediatR;
using System;

namespace Infrastructure.BaseRequest
{
	public abstract class BaseRequest<TResult> : IRequest<TResult>
	{
	}

	public interface IBaseAuthorizeRequest
	{
		public Guid UserId { get; set; }
	}

	public abstract class BaseAuthorizeRequest<TResult> : BaseRequest<TResult>, IBaseAuthorizeRequest
	{
		public Guid UserId { get; set; }
	}

	public interface IBaseGetAuthorizeRequest : IBaseAuthorizeRequest, IWithId
	{

	}

	public abstract class BaseGetAuthorizeRequest<TResult> : BaseAuthorizeRequest<TResult>, IBaseGetAuthorizeRequest
	{
		public Guid Id { get; set; }
	}

	public interface IBaseAuthorizeListRequest : IBaseAuthorizeRequest
	{
		public int Page { get; set; }
		public int Count { get; set; }
		public string Text { get; set; }
	}

	public abstract class BaseAuthorizeListRequest<TResult> : BaseAuthorizeRequest<TResult>, IBaseAuthorizeListRequest
	{
		public int Page { get; set; } = 1;
		public int Count { get; set; } = 5;
		public string Text { get; set; }
		public Ordering Ordering { get; set; } = Ordering.Descending;
	}
}
