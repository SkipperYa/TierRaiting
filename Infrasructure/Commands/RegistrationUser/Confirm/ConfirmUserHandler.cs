using Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class ConfirmUserHandler : BaseHandler<ConfirmUserCommand, bool>
	{
		private readonly IRegistrationService _registrationService;

		public ConfirmUserHandler(IRegistrationService registrationService)
		{
			_registrationService = registrationService;
		}

		public override async Task<bool> Handle(ConfirmUserCommand request, CancellationToken cancellationToken)
		{
			var result = await _registrationService.ConfirmEmail(request.UserId, request.Token, request.Email, cancellationToken);

			return result;
		}
	}
}
