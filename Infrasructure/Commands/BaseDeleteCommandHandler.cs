using AutoMapper;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Commands
{
	public abstract class BaseDeleteCommandHandler<TRequest, TResult> : BaseSaveCommandHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
		where TResult : WithId
	{
		protected override EntityState EntityState => EntityState.Deleted;

		public BaseDeleteCommandHandler(ApplicationContext applicationContext, IMapper mapper)
			: base(applicationContext, mapper)
		{

		}
	}
}
