using System.Collections.Generic;

namespace Domain.Entities
{
	public class PagedList<TEntity>
	{
		public List<TEntity> List { get; set; }
		public int Total { get; set; }
	}
}
