using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Timelogger.Extensions
{
	public class ApiConfiguration
	{
		public string CORS_URLs { get; set; }
		public int PageNumber { get; set; }
		public int PageSize { get; set; }
		public int DatabaseUser { get; set; }
		public bool UseRedis { get; set; }
	}

	[ExcludeFromCodeCoverage]
	public static class CorsConfiguration
	{
		private static readonly string _corsName = "CorsPolicy";

		public static void AddCORSConfiguration(this IServiceCollection services, ApiConfiguration apiConfiguration)
		{
			services.AddCors(options =>
			{
				options.AddPolicy(_corsName, builder =>
					builder.WithOrigins(apiConfiguration.CORS_URLs.Replace(" ", "").Split(","))
					.AllowAnyMethod()
					.AllowAnyHeader());
			});
		}

		public static void EnableCORS(this IApplicationBuilder app)
		{
			app.UseCors(_corsName);
		}
	}
}
