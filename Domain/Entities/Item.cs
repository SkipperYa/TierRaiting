using System.ComponentModel.DataAnnotations.Schema;
using WebApi.Entities;

namespace Domain.Entities
{
	public class Item : WithId
	{
		public string Title { get; set; }
		public string Description { get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }
		public string CategoryId { get; set; }
	}
}
