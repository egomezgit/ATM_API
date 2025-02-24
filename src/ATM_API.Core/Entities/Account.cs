using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Core.Entities
{
    public class Account
    {
        public int Id { get; set; }    
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastWithdrawalDate { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        /// <summary>
        /// Transactions associated with the account
        /// </summary>
        public ICollection<Transaction> Transactions { get; set; } = new List<Transaction>();

    }
}
