using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public class Category : WithId, IWithUserId
	{
		public string Title { get; set; }
		public string Description { get; set; }

		public List<Item> Items { get; set; }

		[ForeignKey(nameof(UserId))]
		public User User { get; set; }
		public Guid UserId { get; set; }

		[ForeignKey(nameof(ImageId))]
		public UserImage<Category> Image { get; set; }
		public Guid? ImageId { get; set; }
	}
}
