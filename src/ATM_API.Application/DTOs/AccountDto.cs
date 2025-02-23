using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.DTOs
{
    public class AccountDto
    {
        public decimal Balance { get; set; }
        public DateTime LastWithdrawalDate { get; set; }
    }
}
