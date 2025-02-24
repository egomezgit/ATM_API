using ATM_API.Application.DTOs.Transaction;
using ATM_API.Application.Interfaces;
using ATM_API.Application.Interfaces.Auth;
using ATM_API.Application.Interfaces.Operation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ATM_API.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly ITransactionHistoryService _transactionHistoryService;
        private readonly IBalanceService _balanceService;

        public TransactionController(ITransactionService transactionService,
            ITransactionHistoryService transactionHistoryService, 
            IBalanceService balanceService)
        {
            _transactionService = transactionService;
            _transactionHistoryService = transactionHistoryService;
            _balanceService = balanceService;
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] TransactionRequestDto transactionDto)
        {
            var response = await _transactionService.HandleAsync(transactionDto);
            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }

        [HttpPost("history")]
        public async Task<IActionResult> GetTransactionHistory([FromBody] TransactionHistoryRequestDto request)
        {
            var response = await _transactionHistoryService.HandleAsync(request);

            if (response == null)
                return NotFound(new { message = "Card not found or blocked." });

            return Ok(response);
        }

        [HttpPost("balance")]
        public async Task<IActionResult> GetBalance([FromBody] string cardNumber)
        {
            var response = await _balanceService.HandleBalanceAsync(cardNumber);
            if (response == null)
                return NotFound(new { message = "Card not found or blocked." });

            return Ok(response);
        }





    }
}
