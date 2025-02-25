using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Core.Entities
{
    public class User
    {
        public int Id { get; set; } 
        public string Name { get; set; }

        /// <summary>
        /// Accounts associated with the user
        /// </summary>
        public ICollection<Account> Accounts { get; set; } = new List<Account>();

    }
}
