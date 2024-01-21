using Domain.Entities;
using Domain.Interfaces;
using Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
	public class RegistrationUserHandler : BaseHandler<RegistrationUserCommand, User>
	{
		private readonly IRegistrationService _registrationService;
		private readonly ILoginService _loginService;

		public RegistrationUserHandler(IRegistrationService registrationService, ILoginService loginService)
		{
			_registrationService = registrationService;
			_loginService = loginService;
		}

		public override async Task<User> Handle(RegistrationUserCommand request, CancellationToken cancellationToken)
		{
			var user = await _registrationService.Registration(request.Email, request.UserName, request.Password, cancellationToken);

			user = await _loginService.Login(new LoginViewModel() { Email = user.Email, Password = request.Password }, cancellationToken);

			return user;
		}
	}
}
