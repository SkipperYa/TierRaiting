using Domain.Interfaces;

namespace Domain.Models
{
	public class UserViewModel : IWithSrc
	{
		public string Id { get; set; }
		public string UserName { get; set; }
		public bool EmailConfirmed { get; set; }
		public string Src { get; set; }
	}

	public class ProfileViewModel : UserViewModel
	{
		public string Email { get; set; }
	}
}
