using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Database
{
	public static class InitialDataSeeder
	{
		public static async Task Initialize(IServiceProvider serviceProvider)
		{
			using var scope = serviceProvider.CreateScope();
			var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
			var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();

			var roles = new List<Role>
			{
				new Role { Name = Role.AdminRole },
				new Role { Name = Role.UserRole }
			};

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role.Name))
				{
					await roleManager.CreateAsync(role);
				}
			}

			var admin = new User()
			{
				UserName = "Admin",
				Email = "admin@admin.com",
				EmailConfirmed = true,
			};

			if (await userManager.FindByEmailAsync(admin.Email) == null)
			{
				var result = await userManager.CreateAsync(admin);

				if (!result.Succeeded)
				{
					throw new InvalidOperationException("Cant seed admin");
				}

				await userManager.AddToRoleAsync(admin, Role.AdminRole);
				await userManager.AddPasswordAsync(admin, "123");
			}
		}
	}
}
