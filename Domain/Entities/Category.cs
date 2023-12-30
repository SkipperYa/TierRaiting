using Domain.Entities;
using System.Collections.Generic;

namespace WebApi.Entities
{
	public class Category
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }

		public List<Item> Items { get; set; }

		public string UserId { get; set; }
		public User User { get; set; }
	}
}
