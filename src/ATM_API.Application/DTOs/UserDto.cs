using ATM_API.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.DTOs
{
    public class UserDto
    {

        public int Id { get; set; }
        /// <summary>
        /// Card associated with the user
        /// </summary>
        public CardDto Card { get; set; }
        //public string CardNumber { get; set; }
        //public string Pin { get; set; }
        public string Name { get; set; }

        /// <summary>
        /// Account associated with the user
        /// </summary>
        public AccountDto Account { get; set; }
    }
}
