using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.Interfaces.Security
{
    public interface IHashingService
    {
        string Hash(string input);
        bool VerifyHash(string input, string hash);
    }
}
