using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Infrastructure.Persistence.Mongo.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace DotNetCoreThreeTier.Infrastructure.Persistence.Mongo
{
    public static class DependencyInjection
    {
        public static IServiceCollection UseMongoPersistence(this IServiceCollection services, IConfiguration config)
        {
            // Bind settings for options pattern
            services.Configure<MongoDbSettings>(config.GetSection("MongoDbSettings"));

            var mongoSettings = config.GetSection("MongoDbSettings").Get<MongoDbSettings>()
                ?? throw new InvalidOperationException("MongoDbSettings not found in configuration.");

            // Mongo client should be singleton
            services.AddSingleton<IMongoClient>(_ => new MongoClient(mongoSettings.ConnectionString));

            // Register MongoDbContext (scoped per request)
            services.AddScoped<MongoDbContext>();

            // Repositories
            services.AddScoped(typeof(IRepository<>), typeof(MongoDbRepository<>));

            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
