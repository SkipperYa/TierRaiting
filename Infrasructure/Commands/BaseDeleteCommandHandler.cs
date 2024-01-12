using AutoMapper;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Commands
{
	public abstract class BaseDeleteCommandHandler<TRequest, TResult> : BaseAuthorizeHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
	{
		protected readonly ApplicationContext _applicationContext;
		protected readonly IMapper _mapper;

		public BaseDeleteCommandHandler(ApplicationContext applicationContext, IMapper mapper)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
		}

		public override async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var entity = _mapper.Map<TResult>(request);

			var item = _applicationContext.Remove(entity);

			item.State = EntityState.Deleted;

			await _applicationContext.SaveChangesAsync(cancellationToken);

			await _applicationContext.DisposeAsync();

			return entity;
		}
	}
}
