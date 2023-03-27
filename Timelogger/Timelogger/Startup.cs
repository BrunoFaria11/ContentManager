using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Timelogger.Entities;
using Timelogger.Extensions;
using Timelogger.Application;
using Microsoft.EntityFrameworkCore;
using Timelogger.Persistance;
using Timelogger.Middlewares;

namespace Timelogger
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;
		public IConfigurationRoot Configuration { get; }

		public Startup(IWebHostEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddPersistance();
			services.AddApplication();

			services.AddControllers();

			services.AddLogging(builder =>
			{ 
				builder.AddConsole();
				builder.AddDebug();
			});

			if (_environment.IsDevelopment())
			{
				services.AddCors();
				services.AddSwaggerGen();
			}
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseCors(builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.SetIsOriginAllowed(origin => true)
					.AllowCredentials());
			}

			app.UseHttpsRedirection();

			app.UseRouting();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			app.UseSwagger();
			app.UseSwaggerUI();
			app.EnableCORS();
			app.UseMiddleware<ErrorHandlerMiddleware>();
			//app.UseMvc();

			var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
			using (var scope = serviceScopeFactory.CreateScope())
			{
				SeedDatabase(scope);
			}
		}

		private static void SeedDatabase(IServiceScope scope)
		{
		    try
		    {
				var context = scope.ServiceProvider.GetService<ApiContext>();
				var testProject1 = new Project
				{
					Id = "1",
					Name = "e-conomic Interview"
				};
		
				context.Projects.Add(testProject1);
		
				context.SaveChanges();
			}
		    catch (System.Exception ex)
		    {
		
		    }
		
		}
	}
}