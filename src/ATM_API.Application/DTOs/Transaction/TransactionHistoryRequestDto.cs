using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.DTOs.Transaction
{
    public class TransactionHistoryRequestDto
    {
        public string CardNumber { get; set; }
        public int Page { get; set; } = 1;  // Página por defecto en 1
    }
}
