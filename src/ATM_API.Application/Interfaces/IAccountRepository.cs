using ATM_API.Core.Entities;

namespace ATM_API.Application.Interfaces
{
    public interface IAccountRepository
    {
        Task<Account?> GetByIdAsync(int accountId);
        Task UpdateAsync(Account account);

        Task<Account?> GetAccountWithUserAsync(int accountId);
    }
}
