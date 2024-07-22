using Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
	public class LogoutCommandHandler : BaseHandler<LogoutCommand, bool>
	{
		private ILoginService _loginService;

		public LogoutCommandHandler(ILoginService loginService)
		{
			_loginService = loginService;
		}

		public override async Task<bool> Handle(LogoutCommand request, CancellationToken cancellationToken)
		{
			await _loginService.Logout(cancellationToken);

			return true;
		}
	}
}
