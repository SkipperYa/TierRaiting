using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
	public abstract class WithId
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public string Id { get; set; }
	}
}
