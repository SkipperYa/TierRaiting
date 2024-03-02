using Domain.Enum;
using Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Item : WithId, IWithSrc
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public Tier Tier { get; set; }

		public string Src { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }
		public Guid CategoryId { get; set; }
	}
}
