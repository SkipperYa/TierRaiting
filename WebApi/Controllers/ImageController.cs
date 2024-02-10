using Azure.Core;
using Domain.Entities;
using Domain.Exceptions;
using Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
	[Authorize]
	public class ImageController : BaseApplicationController
	{
		private ApplicationContext _context;

		public ImageController(IMediator mediator, ApplicationContext context) : base(mediator)
		{
			_context = context;
		}

		[HttpPost]
		public async Task<IActionResult> Upload(CancellationToken cancellationToken)
		{
			var files = HttpContext.Request.Form.Files;

			if (!files.Any())
			{
				throw new LogicException("Invalid image.");
			}

			var guid = Guid.NewGuid().ToString();

			var directory = Path.Combine("images", UserId.ToString(), guid);

			Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), directory));

			var image = files[0];

			var src = Path.Combine(directory, image.FileName);

			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), src);

			using (var fileStream = new FileStream(imagePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream, cancellationToken);
			}

			var userImage = new UserImage<Category>()
			{
				Src = src
			};

			await _context.AddAsync(userImage, cancellationToken);

			await _context.SaveChangesAsync(cancellationToken);

			await _context.DisposeAsync();

			return Ok(userImage);
		}
	}
}
