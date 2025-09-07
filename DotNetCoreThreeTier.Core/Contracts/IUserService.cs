using DotNetCoreThreeTier.Core.Dtos;
using DotNetCoreThreeTier.Core.Entities;

namespace DotNetCoreThreeTier.Core.Contracts
{
    public interface IUserService
    {
        Task<ServiceResponse> GetByIdAsync(int id);
        Task<ServiceResponse> GetAllAsync();
        Task<ServiceResponse> CreateAsync(User user);
        Task<ServiceResponse> UpdateAsync(User user);
        Task<ServiceResponse> DeleteAsync(int id);
    }
}
