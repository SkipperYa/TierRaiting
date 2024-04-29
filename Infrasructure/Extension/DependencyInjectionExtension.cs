using Domain.Interfaces;
using FluentValidation;
using Infrastructure.Behaviors;
using Infrastructure.Commands;
using Infrastructure.Commands.RegistrationUser.Create;
using Infrastructure.Database;
using Infrastructure.Mapper;
using Infrastructure.Queries;
using Infrastructure.Services;
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
			services
				.AddScoped<IValidator<CategoriesQuery>, CategoriesQueryValidator>()
				.AddScoped<IValidator<GetCategoryQuery>, GetCategoryQueryValidator>()
				.AddScoped<IValidator<CreateCategoryCommand>, CreateCategoryCommandValidator>()
				.AddScoped<IValidator<UpdateCategoryCommand>, UpdateCategoryCommandValidator>()
				.AddScoped<IValidator<DeleteCategoryCommand>, DeleteCategoryCommandValidator>()
				.AddScoped<IValidator<RegistrationUserCommand>, RegistrationUserCommandValidator>()
				.AddScoped<IValidator<GetItemsQuery>, GetItemsQueryValidator>()
				.AddScoped<IValidator<GetItemQuery>, GetItemQueryValidator>()
				.AddScoped<IValidator<CreateItemCommand>, CreateItemCommandValidator>()
				.AddScoped<IValidator<UpdateItemCommand>, UpdateItemCommandValidator>()
				.AddScoped<IValidator<DeleteItemCommand>, DeleteItemCommandValidator>()
				.AddScoped<IValidator<GetWeatherForecastQuery>, GetWeatherForecastQueryValidator>()
				.AddScoped<IValidator<GetItemOptionsQuery>, GetItemOptionsQueryValidator>()
				.AddScoped<IValidator<ConfirmUserCommand>, ConfirmUserCommandValidator>()
				.AddScoped<IValidator<SendConfirmCommand>, SendConfirmCommandValidator>()
				;

			return services;
		}

		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services
				.AddTransient<IRegistrationService, RegistrationService>()
				.AddTransient<ILoginService, LoginService>()
				.AddTransient<ISteamService, SteamService>()
				.AddTransient<IBooksService, BooksService>()
				.AddTransient<IImageService, ImageService>()
#if DEBUG
				.AddTransient<IEmailService, ConsoleEmailService>()
#else
				.AddTransient<IEmailService, EmailService>()
#endif
				;

			return services;
		}

		public static IServiceCollection AddAutoMapper(this IServiceCollection services)
		{
			services.AddAutoMapper(config =>
			{
				config.AddProfile<MapperProfile>();
			});

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
