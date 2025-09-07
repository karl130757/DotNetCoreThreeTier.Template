using DotNetCoreThreeTier.Core.Entities;


namespace DotNetCoreThreeTier.Core.Contracts
{
    public interface IUserRepository : IRepository<User>
    {
        // You can add user-specific queries here, e.g.:
        Task<User?> GetByEmailAsync(string email);
    }
}
