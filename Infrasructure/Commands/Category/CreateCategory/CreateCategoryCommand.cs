using Infrastructure.BaseRequest;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Enum;
using Infrastructure.Mapper;
using Domain.Models;

namespace Infrastructure.Commands
{
	public class CreateCategoryCommand : BaseAuthorizeRequest<Category>, IWithSrc, IViewModel
	{
		public string Title { get; set; }
		public string Description { get; set; }
		public CategoryType CategoryType { get; set; }
		public string Src { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<Category, CreateCategoryCommand>().ReverseMap();
		}
	}
}
