using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.BaseRequest;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
	public class BaseListQueryHandler<TQuery, TEntity, TResult> : BaseAuthorizeHandler<TQuery, PagedList<TResult>>
		where TQuery : BaseAuthorizeListRequest<PagedList<TResult>>
		where TEntity : WithId, IWithUserId
	{
		private readonly ApplicationContext _context;
		private readonly IMapper _mapper;

		public BaseListQueryHandler(ApplicationContext context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

		public override async Task<PagedList<TResult>> Handle(TQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Set<TEntity>()
				.AsNoTracking()
				.Where(q => q.UserId == request.UserId);

			query = await Filters(query, request, cancellationToken);

			var count = query.Count();

			var result = await _mapper.ProjectTo<TResult>(query
					.Skip(request.Page - 1)
					.Take(request.Count))
				.ToListAsync();

			return new PagedList<TResult>()
			{
				List = result,
				Total = count
			};
		}

		public virtual Task<IQueryable<TEntity>> Filters(IQueryable<TEntity> query, TQuery request, CancellationToken cancellationToken)
		{
			return Task.FromResult(query);
		}
	}
}
