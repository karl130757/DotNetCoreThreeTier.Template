using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Core.Dtos;

namespace DotNetCoreThreeTier.Application.User
{
    public class UserHandler : IUserHandler
    {
        private readonly IUserRepository _userRepository;

        public UserHandler(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }

        public async Task<HandlerResponse> CreateAsync(Core.Entities.User user)
        {
            await _userRepository.AddAsync(user);
            return new HandlerResponse(true, "User has been created successfully!.", null);
        }

        public Task<HandlerResponse> DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<HandlerResponse> GetAllAsync()
        {
            var employee = await _userRepository.GetAllAsync();
            return new HandlerResponse(true, "success", new { employee });
        }

        public Task<HandlerResponse> GetByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<HandlerResponse> UpdateAsync(Core.Entities.User user)
        {
            throw new NotImplementedException();
        }
    }
}
