using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Database
{
	public class Role : IdentityRole<Guid>
	{

	}

	public class ApplicationContext : IdentityDbContext<User, Role, Guid>
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
		{

		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder
				.Entity<Category>()
				.HasMany(e => e.Items)
				.WithOne(e => e.Category)
				.OnDelete(DeleteBehavior.Cascade);

			base.OnModelCreating(modelBuilder);
		}
	}
}
