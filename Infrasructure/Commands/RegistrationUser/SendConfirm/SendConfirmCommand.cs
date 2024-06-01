using Infrastructure.BaseRequest;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class SendConfirmCommand : BaseRequest<bool>
	{
		public string UserId { get; set; }
		public string Email { get; set; }
	}
}
