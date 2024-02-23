using Domain.Entities;
using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.AspNetCore.Http;
using Domain.Interfaces;

namespace Infrastructure.Interfaces
{
	public interface IUserImageService
	{
		public Task<string> Upload(IFormFile image, Guid userId, CancellationToken cancellationToken);
		public Task AttachImage<TResult>(TResult entity, Guid userId, string src, CancellationToken cancellationToken)
			where TResult : WithId, IWithUserImage<TResult>;
	}
}
