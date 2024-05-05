using AutoMapper;
using Domain.Models;
using Infrastructure.Commands.Profile;
using Infrastructure.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers.Profile
{
	[Authorize]
	public class ProfileController : BaseApplicationController
	{
		private readonly IMapper _mapper;

		public ProfileController(IMediator mediator, IMapper mapper) : base(mediator)
		{
			_mapper = mapper;
		}

		[HttpGet]
		public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new GetUserQuery()
			{
				Id = UserId,
				UserId = UserId,
			}, cancellationToken);

			return Ok(result);
		}

		[HttpPut]
		public async Task<IActionResult> UpdateUser([FromBody] ProfileViewModel profile, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new UpdateUserCommand()
			{
				Email = profile.Email,
				UserName = profile.UserName,
				Src = profile.Src,
				UserId = UserId
			}, cancellationToken);

			var userViewModel = _mapper.Map<ProfileViewModel>(result);

			return Ok(userViewModel);
		}
	}
}
