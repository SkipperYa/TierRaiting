using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Commands;
using Infrastructure.Mapper;

namespace Domain.Models
{
	public class UserViewModel : IWithSrc, IViewModel
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public bool EmailConfirmed { get; set; }
		public string Src { get; set; }

		public void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<User, UserViewModel>().ReverseMap();
		}
	}

	public class ProfileViewModel : UserViewModel, IViewModel
	{
		public string Email { get; set; }

		public new void Mapping(AutoMapper.Profile profile)
		{
			profile.CreateMap<User, ProfileViewModel>().ReverseMap();
		}
	}
}
