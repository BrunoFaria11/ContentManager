using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace ContentManager.Persistance.Helpers
{
    [ExcludeFromCodeCoverage]
    public static class RepositoryHelpers
    {
        private const string CONNECTION_STRING_KEY = "ConnectionString";

        public static string GetConnectionString(IConfiguration configuration)
        {
            return configuration.GetConnectionString(CONNECTION_STRING_KEY);
        }
    }
}
