using ATM_API.Core.Entities;

namespace ATM_API.Application.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetByAccountIdAsync(int accountId, int page, int pageSize);
        Task<Transaction?> GetLastTransactionAsync(int id);
        Task<int> GetTotalTransactionsAsync(int accountId);
    }
}
