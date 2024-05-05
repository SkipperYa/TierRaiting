using AutoMapper;
using Domain.Interfaces;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
	public class BaseGetQueryHandler<TQuery, TEntity, TResult> : BaseAuthorizeHandler<TQuery, TResult>
		where TQuery : BaseGetAuthorizeRequest<TResult>, IBaseGetAuthorizeRequest
		where TEntity : class, IWithId
	{
		protected readonly ApplicationContext _applicationContext;
		protected readonly IMapper _mapper;

		public BaseGetQueryHandler(ApplicationContext applicationContext, IMapper mapper)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
		}

		public virtual Task<IQueryable<TEntity>> Filter(IQueryable<TEntity> query, TQuery request, CancellationToken cancellationToken)
			=> Task.FromResult(query);

		public override async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
		{
			var query = _applicationContext.Set<TEntity>()
				.AsNoTracking()
				.Where(q => q.Id == request.Id);

			query = await Filter(query, request, cancellationToken);

			var entity = await _mapper.ProjectTo<TResult>(query)
				.FirstOrDefaultAsync(cancellationToken);

			return entity;
		}
	}
}
