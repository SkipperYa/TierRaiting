using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Infrastructure.Queries
{
	public class GetCategoryHandler : BaseGetQueryHandler<GetCategoryQuery, Category, CategoryViewModel>
	{
		public GetCategoryHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public override Task<IQueryable<Category>> Filter(IQueryable<Category> query, GetCategoryQuery request, CancellationToken cancellationToken)
		{
			query = query.Where(q => q.UserId == request.UserId);

			return base.Filter(query, request, cancellationToken);
		}
	}
}
