using System.Threading.Tasks;
using ATM_API.Core.Entities;

namespace ATM_API.Core.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
    }
}
