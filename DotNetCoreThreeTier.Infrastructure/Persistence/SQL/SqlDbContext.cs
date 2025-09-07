using DotNetCoreThreeTier.Core.Entities;
using Microsoft.EntityFrameworkCore;


namespace DotNetCoreThreeTier.Infrastructure.Persistence.SQL
{
    public class SqlDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public SqlDbContext (DbContextOptions<SqlDbContext> options): base(options) { } 

    }
}
