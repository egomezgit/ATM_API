using ATM_API.Application.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.Interfaces.Transaction
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionDto>> GetTransactionsByCardNumberAsync(object cardNumber);
        Task<TransactionDto> GetTransactionByIdAsync(int transactionId);

        /// <summary>
        /// Add a transaction to the database
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns></returns>
        Task<WithdrawResponsetDto> AddTransactionAsync(TransactionDto transaction);
        Task UpdateTransactionAsync(TransactionDto transaction);
        Task DeleteTransactionAsync(int transactionId);

    }
}
