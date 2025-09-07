using DotNetCoreThreeTier.Core.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;


namespace DotNetCoreThreeTier.Infrastructure.Persistence.Mongo
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;

        public MongoDbContext(IMongoClient client, IOptions<MongoDbSettings> options)
        {
            var settings = options.Value;
            _database = client.GetDatabase(settings.DatabaseName);
        }


        // Collection for User
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");

        // Generic way to get any collection
        public IMongoCollection<T> GetCollection<T>(string name) => _database.GetCollection<T>(name);
    }
}
