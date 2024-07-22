using Domain.Models;
using Infrastructure.BaseRequest;

namespace Infrastructure.Commands
{
	public class LoginCommand : BaseRequest<ProfileViewModel>
	{
		public string Email { get; set; }
		public string Password { get; set; }
	}
}
