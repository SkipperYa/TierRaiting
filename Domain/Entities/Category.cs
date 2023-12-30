using Domain.Entities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities
{
	public class Category : WithId
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public List<Item> Items { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
		public string UserId { get; set; }
	}
}
