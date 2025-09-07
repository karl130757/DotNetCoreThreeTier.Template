using DotNetCoreThreeTier.Core.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace DotNetCoreThreeTier.Infrastructure.Persistence.SQL
{
    public class SqlDbRepository<T> : IRepository<T> where T : class
    {
        protected readonly SqlDbContext _context;
        private readonly DbSet<T> _dbSet;

        public SqlDbRepository(SqlDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate).AsNoTracking().ToList();
        }

        public async Task<T?> GetByIdAsync(int id) =>
            await _dbSet.AsNoTracking()
                .FirstOrDefaultAsync(e => EF.Property<int>(e, "Id") == id);

        public async Task<IEnumerable<T>> GetAllAsync() =>
            await _dbSet.AsNoTracking().ToListAsync();

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
