using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using Domain.Entities;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;

namespace Infrastructure.Queries
{
	public class GetItemHandler : BaseGetQueryHandler<GetItemQuery, Item, ItemViewModel>
	{
		public GetItemHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public override Task<IQueryable<Item>> Filter(IQueryable<Item> query, GetItemQuery request, CancellationToken cancellationToken)
		{
			query = query.Where(q => q.Category.UserId == request.UserId);

			return base.Filter(query, request, cancellationToken);
		}
	}
}
