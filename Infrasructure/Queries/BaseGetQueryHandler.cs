using AutoMapper;
using Domain.Entities;
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
		where TEntity : WithId, IWithUserId
	{
		protected readonly ApplicationContext _applicationContext;
		protected readonly IMapper _mapper;

		public BaseGetQueryHandler(ApplicationContext applicationContext, IMapper mapper)
		{
			_applicationContext = applicationContext;
			_mapper = mapper;
		}

		public override async Task<TResult> Handle(TQuery request, CancellationToken cancellationToken)
		{
			var entity = await _mapper.ProjectTo<TResult>(_applicationContext.Set<TEntity>()
					.AsNoTracking()
					.Where(q => q.Id == request.Id && q.UserId == request.UserId))
				.FirstOrDefaultAsync();

			return entity;
		}
	}
}
