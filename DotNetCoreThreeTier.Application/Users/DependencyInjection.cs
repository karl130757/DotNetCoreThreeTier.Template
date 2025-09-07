using DotNetCoreThreeTier.Application.Users.Implementations;
using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Infrastructure.Persistence.SQL.Repositories;
using Microsoft.Extensions.DependencyInjection;


namespace DotNetCoreThreeTier.Application.Users
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
