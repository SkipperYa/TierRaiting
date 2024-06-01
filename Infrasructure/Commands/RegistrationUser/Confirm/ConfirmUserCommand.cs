using Infrastructure.BaseRequest;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class ConfirmUserCommand : BaseRequest<bool>
	{
		public string UserId { get; set; }
		public string Token { get; set; }
		public string Email { get; set; }
	}
}
