using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCoreThreeTier.Infrastructure.Persistence.SQL.Repositories
{
    public class UserRepository : SqlDbGenericRepository<User>, IUserRepository
    {
        public UserRepository(SqlDbContext context) : base(context) { }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}