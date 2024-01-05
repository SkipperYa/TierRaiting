using AutoMapper;
using Domain.Interfaces;
using Domain.Models;
using FluentValidation.Internal;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Login
{
	public class LoginController : BaseApplicationController
	{
		private readonly ILoginService _loginService;
		private readonly IMapper _mapper;

		public LoginController(IMediator mediator, ILoginService loginService, IMapper mapper) : base(mediator)
		{
			_loginService = loginService;
			_mapper = mapper;
		}

		[HttpPost]
		public async Task<IActionResult> Login([FromBody] LoginViewModel model, CancellationToken cancellationToken)
		{
			var user = await _loginService.Login(model, cancellationToken);

			var userViewModel = _mapper.Map<UserViewModel>(user);

			return Ok(userViewModel);
		}

		[HttpGet]
		public async Task<IActionResult> Logout(CancellationToken cancellationToken)
		{
			await _loginService.Logout(cancellationToken);

			return Ok(new { Message = "Ok" });
		}
	}
}
