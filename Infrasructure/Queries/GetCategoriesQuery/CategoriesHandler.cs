using Infrastructure.Database;
using Infrastructure.Queries.GetCategoriesQuery;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApi.Entities;

namespace Infrastructure.Queries.WeatherForecastQuery
{
	public class CategoriesHandler : IRequestHandler<CategoriesQuery, List<Category>>
	{
		private readonly ApplicationContext _context;

		public CategoriesHandler(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<List<Category>> Handle(CategoriesQuery request, CancellationToken cancellationToken)
		{
			var query = _context.Set<Category>()
				.AsNoTracking()
				.Where(q => q.UserId == request.UserId);

			if (!string.IsNullOrEmpty(request.Text))
			{
				query = query.Where(q => q.Title.Contains(request.Text));
			}

			var categories = await query
				.Skip(request.Page - 1)
				.Take(request.Count)
				.ToListAsync();

			return categories;
		}
	}
}
