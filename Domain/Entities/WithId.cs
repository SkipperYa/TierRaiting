using Domain.Interfaces;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public abstract class WithId : IWithId
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public Guid Id { get; set; }
	}
}
