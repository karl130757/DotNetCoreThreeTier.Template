using DotNetCoreThreeTier.Infrastructure.Persistence.Mongo;
using DotNetCoreThreeTier.Infrastructure.Persistence.SQL;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreThreeTier.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // Can swap between SQL or Mongo
            // services.UseMongoPersistence(config);
            services.UseSqlPersistence(config);

            return services;
        }
    }
}
