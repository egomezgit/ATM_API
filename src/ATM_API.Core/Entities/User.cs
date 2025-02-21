using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Core.Entities
{
    // ATM_API.Core/Entities/User.cs
    public class User
    {
        public int Id { get; set; }
        public string CardNumber { get; set; }
        public string Pin { get; set; }
        public string Name { get; set; }
        public string AccountNumber { get; set; }
        public decimal Balance { get; set; }
        public DateTime LastWithdrawalDate { get; set; }
        public bool IsBlocked { get; set; }
    }
}
