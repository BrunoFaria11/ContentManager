using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ContentManager.Application.Common.Behaviours;
using System.Reflection;
using ContentManager.Common.Services;
using ContentManager.Common.Interfaces.Services;

namespace ContentManager.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddTransient<IApplicationService, ApplicationService>();
            services.AddTransient<IModelsService, ModelsService>();
            services.AddTransient<IUserService, UserService>();

            return services;
        }
    }
}