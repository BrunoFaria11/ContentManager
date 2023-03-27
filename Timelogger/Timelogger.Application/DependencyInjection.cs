using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Timelogger.Application.Common.Behaviours;
using System.Reflection;
using Timelogger.Common.Services;
using Timelogger.Common.Interfaces.Services;


namespace Timelogger.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddTransient<IInvoiceService, InvoiceService>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<ITimerHistoryService, TimerHistoryService>();

            return services;
        }
    }
}