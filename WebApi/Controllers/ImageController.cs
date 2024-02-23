using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class ImageController : BaseApplicationController
	{
		private readonly IUserImageService _imageService;

		public ImageController(IMediator mediator, IUserImageService imageService) : base(mediator)
		{
			_imageService = imageService;
		}

		[HttpPost]
		public async Task<IActionResult> Upload(CancellationToken cancellationToken)
		{
			var files = HttpContext.Request.Form.Files;

			if (!files.Any())
			{
				throw new LogicException("Invalid image.");
			}

			var image = files[0];

			var src = await _imageService.Upload(image, UserId, cancellationToken);

			return Ok(new { Src = src });
		}
	}
}
