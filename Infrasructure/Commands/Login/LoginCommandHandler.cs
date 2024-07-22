using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands
{
	public class LoginCommandHandler : BaseHandler<LoginCommand, ProfileViewModel>
	{
		private ILoginService _loginService;
		private IMapper _mapper;

		public LoginCommandHandler(ILoginService loginService, IMapper mapper)
		{
			_loginService = loginService;
			_mapper = mapper;
		}

		public override async Task<ProfileViewModel> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var user = await _loginService.Login(request.Email, request.Password, cancellationToken);

			var userViewModel = _mapper.Map<ProfileViewModel>(user);

			return userViewModel;
		}
	}
}
