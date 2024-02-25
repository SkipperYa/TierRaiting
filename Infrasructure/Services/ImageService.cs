using Domain.Interfaces;
using ImageMagick;
using Infrastructure.Database;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class ImageService : IImageService
	{
		public ImageService(ApplicationContext context)
		{

		}

		public async Task<string> Upload(IFormFile image, Guid userId, CancellationToken cancellationToken)
		{
			var guid = Guid.NewGuid().ToString();

			var directory = Path.Combine("images", userId.ToString(), guid);

			Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), directory));

			var src = Path.Combine(directory, image.FileName);

			var imagePath = Path.Combine(Directory.GetCurrentDirectory(), src);

			using var baseImage = new MagickImage(image.OpenReadStream(), MagickFormat.Png);

			var size = new MagickGeometry(400, 400)
			{
				IgnoreAspectRatio = true
			};

			baseImage.Resize(size);

			await baseImage.WriteAsync(imagePath, cancellationToken);

			return src;
		}
	}
}
