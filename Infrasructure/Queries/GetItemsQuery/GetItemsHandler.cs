using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;

namespace Infrastructure.Queries
{
	public class GetItemsHandler : BaseListQueryHandler<GetItemsQuery, Item, ItemViewModel>
	{
		public GetItemsHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public override Task<IQueryable<Item>> Filters(IQueryable<Item> query, GetItemsQuery request, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrEmpty(request.Text))
			{
				query = query.Where(q => q.Title.Contains(request.Text));
			}

			query = query.Where(q => q.CategoryId == request.CategoryId);

			return base.Filters(query, request, cancellationToken);
		}
	}
}
