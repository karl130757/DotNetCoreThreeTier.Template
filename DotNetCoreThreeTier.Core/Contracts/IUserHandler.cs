using DotNetCoreThreeTier.Core.Dtos;
using DotNetCoreThreeTier.Core.Entities;

namespace DotNetCoreThreeTier.Core.Contracts
{
    public interface IUserHandler
    {
        Task<HandlerResponse> GetByIdAsync(string id);
        Task<HandlerResponse> GetAllAsync();
        Task<HandlerResponse> CreateAsync(User user);
        Task<HandlerResponse> UpdateAsync(User user);
        Task<HandlerResponse> DeleteAsync(string id);
    }
}
