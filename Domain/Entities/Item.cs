using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Item : WithId
	{
		public string Title { get; set; }
		public string Description { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }
		public Guid CategoryId { get; set; }
	}
}
