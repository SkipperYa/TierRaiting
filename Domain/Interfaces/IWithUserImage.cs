using Domain.Entities;
using System;

namespace Domain.Interfaces
{
	public interface IWithImageId : IWithId
	{
		public Guid? ImageId { get; set; }
	}

	public interface IWithUserImage<TEntity> : IWithImageId
		where TEntity : WithId
	{
		public UserImage<TEntity> Image { get; set; }
	}
}
