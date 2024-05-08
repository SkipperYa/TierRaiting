using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Domain.Models;
using Infrastructure.BaseRequest;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	public class CreateItemCommand : BaseAuthorizeRequest<Item>, IWithSrc, IViewModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public string CategoryId { get; set; }
		public Tier Tier { get; set; }
		public string Src { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Item, CreateItemCommand>().ReverseMap();
		}
	}
}
