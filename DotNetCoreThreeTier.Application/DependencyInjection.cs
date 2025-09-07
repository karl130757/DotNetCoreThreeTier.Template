
using DotNetCoreThreeTier.Application.User;
using Microsoft.Extensions.DependencyInjection;

namespace DotNetCoreThreeTier.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddUserModule();
            return services;
        }
    }
}
