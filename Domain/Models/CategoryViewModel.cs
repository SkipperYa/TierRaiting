using Domain.Interfaces;

namespace Domain.Models
{
	public class CategoryViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public int ItemsCount { get; set; }
		public string Src { get; set; }
	}
}
