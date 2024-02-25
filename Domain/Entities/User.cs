using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;

namespace Domain.Entities
{
	public class User : IdentityUser<Guid>
	{
		public List<Category> Categories { get; set; }
	}
}
