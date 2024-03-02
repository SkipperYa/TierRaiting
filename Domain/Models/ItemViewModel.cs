using Domain.Enum;
using Domain.Interfaces;

namespace Domain.Models
{
	public class ItemViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public Tier Tier { get; set; }
		public string Src { get; set; }
	}
}
