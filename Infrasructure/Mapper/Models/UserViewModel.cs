using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Mapper;

namespace Domain.Models
{
	[ViewModel<User, ProfileViewModel>(false)]
	public class ProfileViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public bool EmailConfirmed { get; set; }
		public string Src { get; set; }
		public string Email { get; set; }
	}

	[ViewModel<User, UserViewModel>(false)]
	public class UserViewModel : ProfileViewModel
	{
		public bool LockoutEnabled { get; set; }
	}
}
