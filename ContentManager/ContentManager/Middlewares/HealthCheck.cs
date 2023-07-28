using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading.Tasks;
using System.Threading;
using System;

namespace ContentManager.Middlewares
{
    public class HealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context,
            CancellationToken cancellationToken = default)
        {

            return Task.FromResult(HealthCheckResult.Healthy("Healthy result from MyHealthCheck"));
        }
    }
}
