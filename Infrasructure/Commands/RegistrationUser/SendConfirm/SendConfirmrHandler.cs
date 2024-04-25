using Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class SendConfirmHandler : BaseHandler<SendConfirmCommand, bool>
	{
		private readonly IRegistrationService _registrationService;

		public SendConfirmHandler(IRegistrationService registrationService)
		{
			_registrationService = registrationService;
		}

		public override async Task<bool> Handle(SendConfirmCommand request, CancellationToken cancellationToken)
		{
			await _registrationService.SendConfirmation(request.UserId);

			return true;
		}
	}
}
