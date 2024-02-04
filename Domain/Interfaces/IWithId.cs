using System;

namespace Domain.Interfaces
{
	public interface IWithId
	{
		Guid Id { get; set; }
	}
}
