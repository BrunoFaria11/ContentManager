using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using ContentManager.Application;
using ContentManager.Persistance;
using ContentManager.Middlewares;
using ContentManager.Configurations;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ContentManager
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
            services.AddHealthChecks()
           .AddSqlServer(connectionString: Configuration.GetConnectionString("ConnectionString"), healthQuery: "SELECT 1;", name: "sql", failureStatus: HealthStatus.Degraded, tags: new string[] { "db", "sql", "sqlserver" })
           .AddCheck<HealthCheck>("MyHealthCheck", tags: new[] { "custom" });

            //adding healthchecks UI
            services.AddHealthChecksUI(opt =>
            {
                opt.SetEvaluationTimeInSeconds(15); //time in seconds between check
                opt.MaximumHistoryEntriesPerEndpoint(60); //maximum history of checks
                opt.SetApiMaxActiveRequests(1); //api requests concurrency
                opt.DisableDatabaseMigrations();
                opt.AddHealthCheckEndpoint("default api", "/healthz"); //map health check api
            })
            .AddInMemoryStorage();

            services.AddPersistance(Configuration);
            services.AddApplication();

            services.AddControllers();

            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.AddDebug();
            });

            services.AddCORSConfiguration();

            services.AddSwaggerGen();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseDeveloperExceptionPage();

            app.UseHttpsRedirection();


            app.UseRouting();

            app.EnableCORS();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                //adding endpoint of health check for the health check ui in UI format
                endpoints.MapHealthChecks("/healthz", new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });

                //map healthcheck ui endpoing - default is /healthchecks-ui/
                endpoints.MapHealthChecksUI();
                endpoints.MapControllers();
            });


            app.UseSwagger();
            app.UseSwaggerUI();
            //app.UseMvc();

            //var serviceScopeFactory = app.ApplicationServices.GetService<IServiceScopeFactory>();
        }
    }
}