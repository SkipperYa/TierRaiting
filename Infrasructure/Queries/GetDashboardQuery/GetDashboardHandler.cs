using Domain.Entities;
using Domain.Enum;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Queries
{
	public class CategoryCount
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public int Count { get; set; }
	}

	public class TierCount
	{
		public Tier Tier { get; set; }
		public int Count { get; set; }
	}

	public class CategoryTierCount
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public IEnumerable<TierCount> TierCounts { get; set; }
	}

	public class DashboardResult
	{
		public List<CategoryCount> CategoryCounts { get; set; }
		public List<TierCount> TierCounts { get; set; }
		public List<CategoryTierCount> CategoryTierCount { get; set; }
	}

	public class GetDashboardHandler : BaseAuthorizeHandler<GetDashboardQuery, DashboardResult>
	{
		protected readonly ApplicationContext _applicationContext;

		public GetDashboardHandler(ApplicationContext applicationContext)
		{
			_applicationContext = applicationContext;
		}

		public override async Task<DashboardResult> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
		{
			var query = _applicationContext.Set<Category>()
				.AsNoTracking()
				.Where(q => q.UserId == request.UserId)
				.Include(q => q.Items);

			var result = new DashboardResult();

			result.CategoryCounts = await query
				.GroupBy(q => new { q.Id, q.Title })
				.Select(q => new CategoryCount()
				{
					Id = q.Key.Id,
					Title = q.Key.Title,
					Count = q.SelectMany(i => i.Items).Count(),
				})
				.ToListAsync();

			result.TierCounts = await query
				.SelectMany(q => q.Items)
				.GroupBy(q => q.Tier)
				.Select(q => new TierCount()
				{
					Tier = q.Key,
					Count = q.Count(),
				})
				.OrderByDescending(q => q.Tier)
				.ToListAsync();

			return result;
		}
	}
}
