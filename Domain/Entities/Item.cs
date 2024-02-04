using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Interfaces;

namespace Domain.Entities
{
	public class Item : WithId, IWithUserId
	{
		public string Title { get; set; }
		public string Description { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }
		public Guid CategoryId { get; set; }

		/*[ForeignKey(nameof(UserId))]
		public User User { get; set; }*/
		[NotMapped]
		public Guid UserId { get; set; }
	}
}
