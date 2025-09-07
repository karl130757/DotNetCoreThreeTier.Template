using DotNetCoreThreeTier.Application.Users.Implementations;
using DotNetCoreThreeTier.Core.Contracts;
using Microsoft.Extensions.DependencyInjection;


namespace DotNetCoreThreeTier.Application.Users
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserModule(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            return services;
        }
    }
}
