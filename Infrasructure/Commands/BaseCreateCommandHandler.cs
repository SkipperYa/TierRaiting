using AutoMapper;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using Domain.Entities;

namespace Infrastructure.Commands
{
	public abstract class BaseCreateCommandHandler<TRequest, TResult> : BaseSaveCommandHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
		where TResult : WithId
	{
		public BaseCreateCommandHandler(ApplicationContext applicationContext, IMapper mapper)
			: base(applicationContext, mapper)
		{

		}
	}
}
