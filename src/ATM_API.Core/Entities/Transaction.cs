using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Core.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public int CardId { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        /// <summary>
        /// Account id associated with the transaction
        /// </summary>
        public int AccountId { get; set; } 

        /// <summary>
        /// Account associated with the transaction
        /// </summary>
        public Account Account { get; set; }

    }
}
