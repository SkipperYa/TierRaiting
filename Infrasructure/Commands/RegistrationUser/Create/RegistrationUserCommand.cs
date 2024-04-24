using Domain.Entities;
using Infrastructure.BaseRequest;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class RegistrationUserCommand : BaseRequest<User>
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}
