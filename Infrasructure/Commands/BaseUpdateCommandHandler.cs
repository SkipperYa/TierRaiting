using AutoMapper;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace Infrastructure.Commands
{
	public abstract class BaseUpdateCommandHandler<TRequest, TResult> : BaseSaveCommandHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
		where TResult : WithId
	{
		protected override EntityState EntityState => EntityState.Modified;

		public BaseUpdateCommandHandler(ApplicationContext applicationContext, IMapper mapper)
			: base(applicationContext, mapper)
		{

		}
	}
}
