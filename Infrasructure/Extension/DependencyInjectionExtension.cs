using FluentValidation;
using Infrastructure.Behaviors;
using Infrastructure.Database;
using Infrastructure.Queries.WeatherForecastCommand;
using Infrastructure.Queries.WeatherForecastQuery;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Infrastructure.Extension
{
	public static class DependencyInjectionExtension
	{
		public static IServiceCollection AddMediator(this IServiceCollection services)
		{
			services.AddMediatR(cfg =>
			{
				cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
				cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
				cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
			});

			services.AddValidators();

			return services;
		}

		public static IServiceCollection AddValidators(this IServiceCollection services)
		{
			services.AddScoped<IValidator<GetWeatherForecastQuery>, GetWeatherForecastQueryValidator>();

			return services;
		}

		public static IServiceCollection AddApplicationContext(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<ApplicationContext>(options =>
			{
#if DEBUG
				options.EnableSensitiveDataLogging();
#endif
				options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("WebApi"));
			});

			return services;
		}
	}
}
