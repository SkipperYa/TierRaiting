using Domain.ControllerFilters;
using Domain.Entities;
using Infrastructure.Database;
using Infrastructure.Extension;
using Infrastructure.Services;
using Infrastructure.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Timeouts;
using System;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Authorization;
using WebApi.AuthorizationHandler;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net;

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
				.Configure<EmailOptions>(Configuration.GetSection("Email"))
				.Configure<GoogleApiOptions>(Configuration.GetSection("GoogleApi"))
				.Configure<OMDbOptions>(Configuration.GetSection("OMDbApi"));

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

			// TODO: replace to infrastructure
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

			services.ConfigureApplicationCookie(options =>
			{
				options.Events.OnRedirectToLogin = (context) =>
				{
					context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
					return Task.CompletedTask;
				};
			});

			services.AddMediator();

			services.AddRequestTimeouts(options =>
			{
				options.AddPolicy("DefaultTimeout10s", new RequestTimeoutPolicy()
				{
					Timeout = TimeSpan.FromMilliseconds(10000),
					TimeoutStatusCode = (int)HttpStatusCode.ServiceUnavailable,
				});
			});

			services
				.AddSingleton<IAuthorizationHandler, ProtectFolderAuthorizationHandler>();

			services.AddHttpContextAccessor();

			services.AddServices();

			services.AddHttpClient(BooksService.ClientName, client => 
			{
				// client.BaseAddress = new Uri("https://openlibrary.org/search.json");
				client.BaseAddress = new Uri("https://www.googleapis.com/books/v1/volumes");
			});

			services.AddHttpClient(FilmsService.ClientName, client =>
			{
				client.BaseAddress = new Uri("http://www.omdbapi.com/");
			});

			services.AddResponseCompression(options => {
				options.EnableForHttps = true;
				options.Providers.Add<BrotliCompressionProvider>();
			});

			services.Configure<BrotliCompressionProviderOptions>(options =>
			{
				options.Level = CompressionLevel.Optimal;
			});
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

			app.UseResponseCompression();
			app.UseResponseCaching();

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
