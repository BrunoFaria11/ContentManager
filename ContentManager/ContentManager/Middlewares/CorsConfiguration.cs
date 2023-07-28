using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace ContentManager.Configurations
{
    [ExcludeFromCodeCoverage]
    public static class CorsConfiguration
    {
        private static readonly string _corsName = "CorsPolicy";

        public static IServiceCollection AddCORSConfiguration(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(_corsName, builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });
            return services;
        }

        public static void EnableCORS(this IApplicationBuilder app)
        {
            app.UseCors(_corsName);
        }
    }
}
