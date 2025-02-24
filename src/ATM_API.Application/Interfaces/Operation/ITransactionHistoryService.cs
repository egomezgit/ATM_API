using ATM_API.Application.DTOs.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.Interfaces.Operation
{
    public interface ITransactionHistoryService
    {
        Task<TransactionHistoryResponseDto> HandleAsync(TransactionHistoryRequestDto request);
    }
}
