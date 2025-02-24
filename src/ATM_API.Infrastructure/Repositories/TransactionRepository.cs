using ATM_API.Application.Interfaces;
using ATM_API.Core.Entities;
using ATM_API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ATM_API.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly AppDbContext _context;

        public TransactionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetByAccountIdAsync(int accountId, int page, int pageSize)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalTransactionsAsync(int accountId)
        {
            return await _context.Transactions.CountAsync(t => t.AccountId == accountId);
        }

        public async Task<Transaction?> GetLastTransactionAsync(int accountId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId)
                .OrderByDescending(t => t.Date)
                .FirstOrDefaultAsync();
        }
    }
}

