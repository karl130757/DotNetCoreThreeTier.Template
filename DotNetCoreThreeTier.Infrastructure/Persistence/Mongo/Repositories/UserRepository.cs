
using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Core.Entities;
using MongoDB.Driver;

namespace DotNetCoreThreeTier.Infrastructure.Persistence.Mongo.Repositories
{
    public class UserRepository : MongoDbRepository<User>, IUserRepository
    {
        public UserRepository(MongoDbContext db) : base(db) { }

        public async Task<User?> GetByEmailAsync(string email) =>
            await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
    }
}
