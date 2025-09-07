using DotNetCoreThreeTier.Core.Contracts;
using MongoDB.Driver;
using System.Linq.Expressions;

namespace DotNetCoreThreeTier.Infrastructure.Persistence.Mongo
{
    public class MongoDbRepository<T> : IRepository<T> where T : class
    {
        protected readonly IMongoCollection<T> _collection;

        public MongoDbRepository(MongoDbContext context)
        {
            _collection = context.GetCollection<T>(typeof(T).Name);
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _collection.Find(predicate).ToList();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            return await _collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _collection.Find(Builders<T>.Filter.Empty).ToListAsync();
        }

        public async Task AddAsync(T entity)
        {
            await _collection.InsertOneAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            var idProp = typeof(T).GetProperty("Id");
            if (idProp == null)
                throw new InvalidOperationException($"Entity {typeof(T).Name} does not have an Id property.");

            var idValue = (int)idProp.GetValue(entity)!;
            var filter = Builders<T>.Filter.Eq("Id", idValue);

            await _collection.ReplaceOneAsync(filter, entity, new ReplaceOptions { IsUpsert = false });
        }

        public async Task DeleteAsync(int id)
        {
            var filter = Builders<T>.Filter.Eq("Id", id);
            await _collection.DeleteOneAsync(filter);
        }
    }
}
