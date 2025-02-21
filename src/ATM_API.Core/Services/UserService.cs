using System.Threading.Tasks;
using ATM_API.Core.Entities;
using ATM_API.Core.Interfaces;

namespace ATM_API.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetByIdAsync(userId);
        }
    }
}