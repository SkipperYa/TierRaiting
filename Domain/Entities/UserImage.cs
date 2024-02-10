using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	[Table("UserImage<TEntity>")]
	public class UserImage<TEntity>
		where TEntity : WithId
	{
		public Guid Id { get; set; }

		public string Src { get; set; }

		[ForeignKey(nameof(ObjectId))]
		public TEntity Object { get; set; }
		public Guid? ObjectId { get; set; }
	}
}
