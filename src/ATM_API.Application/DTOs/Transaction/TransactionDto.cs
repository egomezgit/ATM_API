using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.DTOs.Transaction
{
    public class TransactionDto
    {
        /// <summary>
        /// Card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// Amount of the transaction
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Date of the transaction
        /// </summary>
        public DateTime TransactionDate { get; set; }
        /// <summary>
        /// Type of transaction
        /// </summary>
        public string TransactionType { get; set; }

        
    }
}
