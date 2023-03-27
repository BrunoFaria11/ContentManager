using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Application.Common.Behaviours;
using System.Reflection;
using Timelogger.Common.Interfaces.Repositories;
using Timelogger.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Timelogger.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services)
        {
            services.AddTransient<IInvoiceRepository, InvoiceRepository>();
            services.AddTransient<IProjectRepository, ProjectRepository>();
            services.AddTransient<ITimerHistoryRepository, TimerHistoryRepository>();

            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("e-conomic interview"));

            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }


    }
}