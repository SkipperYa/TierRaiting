using System.Threading.Tasks;
using System.Threading;
using System;
using Microsoft.AspNetCore.Http;

namespace Domain.Interfaces
{
	public interface IImageService
	{
		public Task<string> Upload(IFormFile image, Guid userId, CancellationToken cancellationToken);
	}
}
