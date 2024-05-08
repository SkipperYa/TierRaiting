using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Mapper;

namespace Domain.Models
{
	public class ItemViewModel : IWithSrc, IViewModel
	{
		public string Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public Tier Tier { get; set; }
		public string Src { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Item, ItemViewModel>().ReverseMap();
		}
	}
}
