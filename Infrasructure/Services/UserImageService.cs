using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
	public class UserImageService : IUserImageService
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

		public async Task AttachImage<TResult>(TResult entity, Guid userId, string src, CancellationToken cancellationToken)
			where TResult : WithId, IWithUserImage<TResult>
		{
			if (string.IsNullOrEmpty(src))
			{
				return;
			}

			if (string.IsNullOrEmpty(entity.Id.ToString()))
			{
				throw new LogicException($"Entity {typeof(TResult).Name} must be already saved");
			}

			var path = Path.Combine(Directory.GetCurrentDirectory(), src);

			if (!File.Exists(path))
			{
				throw new LogicException($"Invalid path");
			}

			var userImage = new UserImage<TResult>()
			{
				Src = src,
				UserId = userId,
				ObjectId = entity.Id,
			};

			_context.Set<UserImage<TResult>>()
				.AsNoTracking()
				.Where(q => q.UserId == userId && q.ObjectId.HasValue && q.ObjectId.Value == entity.Id)
				.ExecuteUpdate(q => q.SetProperty(i => i.ObjectId, i => null));

			await _context.AddAsync(userImage);
			await _context.SaveChangesAsync();

			_context.Set<TResult>()
				.AsNoTracking()
				.Where(q => q.Id == entity.Id)
				.ExecuteUpdate(q => q.SetProperty(i => i.ImageId, i => userImage.Id));

			entity.Image = userImage;
			entity.ImageId = userImage.Id;
		}
	}
}
