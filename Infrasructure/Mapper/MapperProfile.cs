using AutoMapper;
using Domain.Entities;
using Infrastructure.Commands;
using Infrastructure.Commands.RegistrationUser;
using WebApi.Entities;

namespace Infrastructure.Mapper
{
	public class MapperProfile : Profile
	{
		public MapperProfile()
		{
			CreateMap<User, RegistrationUserCommand>().ReverseMap();
			CreateMap<Category, CreateCategoryCommand>().ReverseMap();
		}
	}
}
