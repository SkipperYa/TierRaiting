using AutoMapper;
using Domain.Entities;
using Domain.Models;
using Infrastructure.Commands;
using System;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			var types = Assembly
				.GetExecutingAssembly()
				.GetExportedTypes()
				.Where(type => type.GetInterfaces()
					.Any(i => !i.IsGenericType && i.IsTypeDefinition && i == typeof(IViewModel)))
				.ToList();

			foreach (var type in types)
			{
				var instance = Activator.CreateInstance(type);
				var methodInfo = type.GetMethod("Mapping");

				methodInfo.Invoke(instance, new object[] { this });
			}

			/*CreateMap<User, RegistrationUserCommand>().ReverseMap();
			CreateMap<User, UserViewModel>().ReverseMap();
			CreateMap<User, ProfileViewModel>().ReverseMap();
			CreateMap<Category, CreateCategoryCommand>().ReverseMap();
			CreateMap<Category, UpdateCategoryCommand>().ReverseMap();
			CreateMap<Category, DeleteCategoryCommand>().ReverseMap();
			CreateMap<Category, CategoryViewModel>().ReverseMap();
			CreateMap<Category, CategoryListViewModel>()
				.ForMember(c => c.ItemsCount, opt => opt.MapFrom(src => src.Items.Count))
				.ReverseMap();
			CreateMap<Item, ItemViewModel>().ReverseMap();
			CreateMap<Item, CreateItemCommand>().ReverseMap();
			CreateMap<Item, UpdateItemCommand>().ReverseMap();
			CreateMap<Item, DeleteItemCommand>().ReverseMap();*/
		}
	}
}
