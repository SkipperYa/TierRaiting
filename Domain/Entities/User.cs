using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System;
using Domain.Interfaces;

namespace Domain.Entities
{
	public class User : IdentityUser<Guid>, IWithId, IWithSrc
	{
		public List<Category> Categories { get; set; }
		public string Src { get; set; }
	}
}
