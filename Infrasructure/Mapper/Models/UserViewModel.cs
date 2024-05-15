using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Mapper;

namespace Domain.Models
{
	[ViewModel<User, UserViewModel>(false)]
	public class UserViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public bool EmailConfirmed { get; set; }
		public string Src { get; set; }
	}

	[ViewModel<User, ProfileViewModel>(false)]
	public class ProfileViewModel : UserViewModel
	{
		public string Email { get; set; }
	}
}
