using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Commands;

namespace Infrastructure.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<User, RegistrationUserCommand>().ReverseMap();
			CreateMap<User, UserViewModel>().ReverseMap();
			CreateMap<Category, CreateCategoryCommand>().ReverseMap();
			CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
			CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
			CreateMap<Category, CategoryViewModel>()
				.ForMember(c => c.ItemsCount, opt => opt.MapFrom(src => src.Items.Count))
				.ReverseMap();
			CreateMap<Item, ItemViewModel>().ReverseMap();
			CreateMap<Item, CreateItemCommand>().ReverseMap();
			CreateMap<Item, UpdateItemCommand>().ReverseMap();
			CreateMap<Item, DeleteItemCommand>().ReverseMap();
		}
	}
}
