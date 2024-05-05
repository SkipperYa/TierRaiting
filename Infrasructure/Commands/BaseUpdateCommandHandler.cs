using AutoMapper;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Domain.Interfaces;

namespace Infrastructure.Commands
{
	public abstract class BaseUpdateCommandHandler<TRequest, TResult> : BaseSaveCommandHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
		where TResult : class, IWithId
	{
		protected override EntityState EntityState => EntityState.Modified;

		public BaseUpdateCommandHandler(ApplicationContext applicationContext, IMapper mapper)
			: base(applicationContext, mapper)
		{

		}
	}
}
