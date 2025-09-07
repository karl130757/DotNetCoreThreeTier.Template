using DotNetCoreThreeTier.Core.Contracts;
using DotNetCoreThreeTier.Core.Dtos;
using DotNetCoreThreeTier.Core.Entities;

namespace DotNetCoreThreeTier.Application.Users.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<ServiceResponse> CreateAsync(User user)
        {
            var data = _userRepository.Find(f => f.Email!.ToLower() == user.Email!.ToLower());

            if (data.Any())
            {
                return new ServiceResponse(false, "User already exist!.", null);
            }

            await _userRepository.AddAsync(user);

            return new ServiceResponse(true, "User has been added!", null);
        }

        public async Task<ServiceResponse> DeleteAsync(int id)
        {
            var data = _userRepository.Find(f => f.Id == id);

            if (!data.Any())
            {
                return new ServiceResponse(false, "User not found!.", null);
            }

            await _userRepository.DeleteAsync(id);

            return new ServiceResponse(true, "User has been removed!", null);

        }

        public async Task<ServiceResponse> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return new ServiceResponse(true, "success", new { users });
        }

        public async Task<ServiceResponse> GetByIdAsync(int id)
        {
            var data = _userRepository.Find(f => f.Id == id);

            if (!data.Any())
            {
                return new ServiceResponse(false, "User not found!.", null);
            }

            var user = await _userRepository.GetByIdAsync(id);

            return new ServiceResponse(true, "success", new { user });
        }

        public async Task<ServiceResponse> UpdateAsync(User user)
        {
            var data = _userRepository.Find(f => f.Id == user.Id);

            if (!data.Any())
            {
                return new ServiceResponse(false, "User not found!.", null);
            }

            await _userRepository.UpdateAsync(user);

            return new ServiceResponse(true, "User has been updated!", null);
        }
    }
}
