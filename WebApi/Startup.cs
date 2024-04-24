using Domain.ControllerFilters;
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Database;
using Infrastructure.Extension;
using Infrastructure.Services;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Http.Timeouts;
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using WebApi.AuthorizationHandler;
using Microsoft.AspNetCore.Identity;

namespace WebApi
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services
				.Configure<HostUrlOptions>(Configuration.GetSection("Host"))
				.Configure<EmailOptions>(Configuration.GetSection("Email"));

			services.AddControllers(options =>
			{
				options.Filters.Add(typeof(LogicExceptionFilter));
			});

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/build";
			});

			services.AddApplicationContext(Configuration);

			services.AddAutoMapper();

			//TODO: replace to infrastructure
			services.AddIdentity<User, Role>(options =>
			{
				options.User.RequireUniqueEmail = true;
				options.SignIn.RequireConfirmedEmail = true;
				options.Password.RequireDigit = true;
				options.Password.RequiredLength = 1;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireUppercase = false;
			}).AddEntityFrameworkStores<ApplicationContext>()
			.AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider);

			services.AddMediator();

			services.AddRequestTimeouts(options =>
			{
				options.AddPolicy("DefaultTimeout10s", new RequestTimeoutPolicy()
				{
					Timeout = TimeSpan.FromMilliseconds(10000),
					TimeoutStatusCode = 503,
				});
			});

			services
				.AddAuthentication(config =>
				{
					config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
					config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				})
				.AddJwtBearer("Bearer", options =>
				{
					options.RequireHttpsMetadata = false;
					options.SaveToken = true;
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidIssuer = AuthOptions.ISSUER,
						ValidateAudience = true,
						ValidAudience = AuthOptions.AUDIENCE,
						ValidateLifetime = true,
						IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
						ValidateIssuerSigningKey = true,
					};
				});

			services
				.AddSingleton<IAuthorizationHandler, ProtectFolderAuthorizationHandler>();

			services.AddHttpContextAccessor();

			services.AddServices();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseRequestTimeouts();

			app.Use(async (context, next) =>
			{
				// MIME sniffing
				context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
				// XSS
				context.Response.Headers.Append("X-Xss-Protection", "1");
				// clickjacking
				context.Response.Headers.Append("X-Frame-Options", "DENY");

				await next();
			});

			app.Use(async (context, next) =>
			{
				var token = context.Request.Cookies[AuthOptions.TOKENNAME];
				if (!string.IsNullOrEmpty(token))
					context.Request.Headers.Append("Authorization", "Bearer " + token);

				await next();
			});

			app.UseAuthentication();
			app.UseAuthorization();

			var root = Directory.GetCurrentDirectory();

			app.UseMiddleware<ProtectFolder>(new ProtectFolderOptions()
			{
				Requirement = new ProtectFolderAuthorizationRequirement()
			});

			app.UseStaticFiles(new StaticFileOptions()
			{
				FileProvider = new PhysicalFileProvider(Path.Combine(root, "images")),
				RequestPath = new PathString(Path.Combine(root, "/images")),
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}"
				);
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}
	}
}
