using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Infrastructure.Persistence.SQL.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DotNetCoreThreeTier.Infrastructure.Persistence.SQL
{
    public static class DependencyInjection
    {
        public static IServiceCollection UseSqlPersistence(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<SqlDbContext>(options =>
                options.UseSqlServer(config.GetConnectionString("SqlServerConnection")));

            services.AddScoped(typeof(IRepository<>), typeof(SqlDbRepository<>));
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
