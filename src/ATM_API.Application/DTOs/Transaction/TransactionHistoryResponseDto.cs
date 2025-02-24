using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.DTOs.Transaction
{
    public class TransactionHistoryResponseDto
    {
        public int TotalRecords { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public List<TransactionDto> Transactions { get; set; }
    }
}
