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

		/// <summary>
		/// Get User for client side. Can be called without authentication cookie.
		/// </summary>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with <see cref="ProfileViewModel" /> or null if user not sign in</returns>
		[HttpGet]
		[AllowAnonymous]
		public async Task<IActionResult> GetUser(CancellationToken cancellationToken)
		{
			var result = IsAuthenticated
				? await _mediator.Send(new GetUserQuery()
					{
						Id = UserId,
						UserId = UserId,
					}, cancellationToken)
				: null;

			return Ok(result);
		}

		/// <summary>
		/// Update user
		/// </summary>
		/// <param name="profile">Class with updated fields</param>
		/// <param name="cancellationToken">CancellationToken</param>
		/// <returns>Return <see cref="OkObjectResult">Ok</see> with updated <see cref="ProfileViewModel" /></returns>
		[HttpPut]
		public async Task<IActionResult> UpdateUser([FromBody] ProfileViewModel profile, CancellationToken cancellationToken)
		{
			var result = await _mediator.Send(new UpdateUserCommand()
			{
				Email = profile.Email,
				UserName = profile.UserName,
				Src = profile.Src,
			}, cancellationToken);

			var userViewModel = _mapper.Map<ProfileViewModel>(result);

			return Ok(userViewModel);
		}
	}
}
