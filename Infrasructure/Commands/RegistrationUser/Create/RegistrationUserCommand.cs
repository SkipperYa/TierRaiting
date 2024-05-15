using Domain.Entities;
using Infrastructure.BaseRequest;
using Infrastructure.Mapper;

namespace Infrastructure.Commands
{
	[ViewModel<User, RegistrationUserCommand>(false)]
	public class RegistrationUserCommand : BaseRequest<User>
	{
		public string UserName { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string PasswordConfirm { get; set; }
	}
}
