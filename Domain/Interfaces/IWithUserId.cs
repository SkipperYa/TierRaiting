using System;

namespace Domain.Interfaces
{
	public interface IWithUserId
	{
		public Guid UserId { get; set; }
	}
}
