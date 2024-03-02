using Domain.Enum;
using Domain.Interfaces;
using System.Collections.Generic;

namespace Domain.Models
{
	public class CategoryViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ItemsCount { get; set; }
		public string Src { get; set; }
		public List<ItemViewModel> Items { get; set; }
	}

	public class CategoryListViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ItemsCount { get; set; }
		public string Src { get; set; }
	}
}
