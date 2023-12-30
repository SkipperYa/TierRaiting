using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using WebApi.Entities;

namespace Domain.Entities
{
	public class User : IdentityUser
	{
		public List<Category> Categories { get; set; }
	}
}
