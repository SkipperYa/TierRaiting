using Infrastructure.Database;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.Linq;

namespace WebApi
{
	public static class ProgramExtension
	{
		public static IHost Deploy(this IHost host)
		{
			using var scope = host.Services.CreateScope();

			using var context = scope.ServiceProvider.GetService<ApplicationContext>();

			if (context.Database.GetPendingMigrations().Any())
			{
				context.Database.Migrate();
			}

			return host;
		}
	}

	public class Program
	{
		public static void Main(string[] args)
		{
			Log.Logger = new LoggerConfiguration()
				.MinimumLevel.Debug()
				.WriteTo.File($"logs/log.txt", rollingInterval: RollingInterval.Day)
				.WriteTo.Console()
				.CreateLogger();

			CreateHostBuilder(args)
				.Build()
				.Deploy()
				.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
