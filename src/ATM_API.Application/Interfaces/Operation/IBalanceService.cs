using ATM_API.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.Interfaces.Operation
{
    public interface IBalanceService
    {
        Task<BalanceResponseDto> HandleBalanceAsync(string cardNumber);
    }
}
