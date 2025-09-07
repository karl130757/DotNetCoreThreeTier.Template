using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace DotNetCoreThreeTier.Infrastructure.Persistence.SQL.Repositories
{
    public class UserRepository : SqlDbRepository<User>, IUserRepository
    {
        public UserRepository(SqlDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}