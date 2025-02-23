using System.Threading.Tasks;
using ATM_API.Application.DTOs;
using ATM_API.Core.Entities;

namespace ATM_API.Application.Interfaces
{
    public interface IUserService
    {
        Task <UserDto> GetUserByCardNumberAsync(object cardNumber);
        Task<User> GetUserByIdAsync(int userId);
        Task UpdateUserAsync(UserDto user);
    }
}
