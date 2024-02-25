using Domain.Interfaces;
using Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class UserImageService : IImageService
	{
		private readonly ApplicationContext _context;

		public UserImageService(ApplicationContext context)
		{
			_context = context;
		}

		public async Task<string> Upload(IFormFile image, Guid userId, CancellationToken cancellationToken)
		{
			var guid = Guid.NewGuid().ToString();

			var directory = Path.Combine("images", userId.ToString(), guid);

			Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), directory));

			var src = Path.Combine(directory, image.FileName);

			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), src);

			using (var fileStream = new FileStream(imagePath, FileMode.Create))
			{
				await image.CopyToAsync(fileStream, cancellationToken);
			}

			return src;
		}
	}
}
