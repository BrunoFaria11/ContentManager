using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Common.Interfaces.Services;
using Timelogger.Repositories;

namespace Timelogger.Api.Test.Tools
{
    [ExcludeFromCodeCoverage]
    public class CustomWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        public TestDbContext DbContextTest { get; private set; }
        public Mock<IProjectService> ProjectServiceMock { get; private set; }

        public IUnitOfWork UnitOfWork { get; private set; }

        public CustomWebApplicationFactory()
        {
            DbContextTest = new TestDbContext();
            ProjectServiceMock = new Mock<IProjectService>();
          
            UnitOfWork = new UnitOfWork(DbContextTest);
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Remove the app's StoreContext registration.
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<ApiContext>));

                if (descriptor != null)
                {
                    services.Remove(descriptor);
                }

                var unitOfWorkDescriptor = services.SingleOrDefault(c => c.ServiceType == typeof(IUnitOfWork));
                if (unitOfWorkDescriptor != null)
                {
                    services.Remove(unitOfWorkDescriptor);
                    services.AddTransient<IUnitOfWork>(serviceProvider => { return UnitOfWork; });
                }

                services.AddTransient<ApiContext, TestDbContext>(sp =>
                {
                    return DbContextTest;
                });
                // Add StoreContext using an in-memory database for testing.
                services.AddDbContext<ApiContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryDbForFunctionalTesting");
                });

                var projectServiceClientDescriptor = services.SingleOrDefault(c => c.ServiceType == typeof(IProjectService));
                if (projectServiceClientDescriptor != null)
                {
                    services.Remove(projectServiceClientDescriptor);
                    services.AddTransient<IProjectService>(serviceProvider => { return ProjectServiceMock.Object; });
                }
           
            });
        }

        public void CustomConfigureServices(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Get service provider.
                var serviceProvider = services.BuildServiceProvider();

                using (var scope = serviceProvider.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;

                    var storeDbContext = scopedServices.GetRequiredService<ApiContext>();
                }
            });
        }
    }
}

