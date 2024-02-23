using System;

namespace Domain.Models
{
	public class CategoryViewModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ItemsCount { get; set; }

		public UserImageViewModel Image { get; set; }
		public Guid? ImageId { get; set; }
	}
}
