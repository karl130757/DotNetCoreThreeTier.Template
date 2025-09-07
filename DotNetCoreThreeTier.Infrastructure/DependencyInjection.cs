using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Infrastructure.Persistence.SQL;
using DotNetCoreThreeTier.Infrastructure.Persistence.SQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreThreeTier.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            // SQL Server (EF Core)
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("SqlServerConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(SqlDbGenericRepository<>)); 

            return services;
        }
    }
}
