using Domain.Entities;
using Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands.RegistrationUser.Create
{
	public class RegistrationUserHandler : BaseHandler<RegistrationUserCommand, User>
	{
		private readonly IRegistrationService _registrationService;

		public RegistrationUserHandler(IRegistrationService registrationService)
		{
			_registrationService = registrationService;
		}

		public override async Task<User> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _registrationService.Registration(request.Email, request.UserName, request.Password, cancellationToken);

			return user;
		}
	}
}
