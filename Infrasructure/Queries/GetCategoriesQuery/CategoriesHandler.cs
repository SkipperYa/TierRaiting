using AutoMapper;
using Domain.Models;
using Infrastructure.Database;
using Infrastructure.Queries.GetCategoriesQuery;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Entities;

namespace Infrastructure.Queries.WeatherForecastQuery
{
	public class CategoriesHandler : BaseListQuery<CategoriesQuery, Category, CategoryViewModel>
	{
		public CategoriesHandler(ApplicationContext context, IMapper mapper) : base(context, mapper)
		{
		}

		public override Task<IQueryable<Category>> Filters(IQueryable<Category> query, CategoriesQuery request, CancellationToken cancellationToken)
		{
			if (!string.IsNullOrEmpty(request.Text))
			{
				query = query.Where(q => q.Title.Contains(request.Text));
			}

			return base.Filters(query, request, cancellationToken);
		}
	}
}
