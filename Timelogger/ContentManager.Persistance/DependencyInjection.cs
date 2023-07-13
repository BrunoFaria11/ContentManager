using Microsoft.Extensions.DependencyInjection;
using ContentManager.Common.Interfaces.Repositories;
using ContentManager.Repositories;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContentManager.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            
            var connectionString = "Server = mssql-134346-0.cloudclusters.net,10136; Database = CM; User Id = Bruno;Password = TTTuuu987;Encrypt=True;TrustServerCertificate=True;";

            services.AddDbContext<CMDbContext>(opt =>
            {
                opt.UseSqlServer(connectionString,
                    sqlServerOptionsAction: sqlOptions =>
                    {
                        sqlOptions.EnableRetryOnFailure(
                            maxRetryCount: 5,
                            maxRetryDelay: TimeSpan.FromSeconds(10),
                            errorNumbersToAdd: null);
                        sqlOptions.MigrationsAssembly(typeof(CMDbContext).Assembly.FullName);
                    });
            });

            

            services.AddScoped<ICMDbContext>(provider => provider.GetService<CMDbContext>());
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }


    }
}