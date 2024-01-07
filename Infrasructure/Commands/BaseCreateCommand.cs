using AutoMapper;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Commands
{
	public abstract class BaseCreateCommand<TRequest, TResult> : BaseAuthorizeHandler<TRequest, TResult>
		where TRequest : BaseAuthorizeRequest<TResult>
	{
		protected readonly ApplicationContext _applicationContext;
		protected readonly IMapper _mapper;

		public BaseCreateCommand(ApplicationContext applicationContext, IMapper mapper)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
		}

		public override async Task<TResult> Handle(TRequest request, CancellationToken cancellationToken)
		{
			var entity = _mapper.Map<TResult>(request);

			var item = await _applicationContext.AddAsync(entity, cancellationToken);

			item.State = EntityState.Added;

			await _applicationContext.SaveChangesAsync(cancellationToken);

			await _applicationContext.DisposeAsync();

			return entity;
		}
	}
}
