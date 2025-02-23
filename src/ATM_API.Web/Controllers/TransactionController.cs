using ATM_API.Application.DTOs.Transaction;
using ATM_API.Application.Interfaces;
using ATM_API.Application.Interfaces.Auth;
using ATM_API.Application.Interfaces.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace ATM_API.Web.Controllers
{
    // Controllers/TransactionController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly ITransactionService _transactionService;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(IUserService userService , ITransactionService transactionRepository)
        {
            _transactionService = transactionRepository;
            _userService = userService;
        }

        [HttpPost("withdraw")]
        public async Task<IActionResult> Withdraw([FromBody] WithdrawRequestDto request)
        {
            var user = await _userService.GetUserByCardNumberAsync(request.CardNumber);
            if (user == null || user.Card.IsBlocked)
                return Unauthorized("Card blocked or does not exist.");

            if (user.Account.Balance < request.Amount)
                return BadRequest("Insufficient funds.");

            user.Account.Balance -= request.Amount;
            user.Account.LastWithdrawalDate = DateTime.UtcNow;

            var transaction = new TransactionDto
            {
                CardNumber = request.CardNumber,
                Amount = request.Amount,
                TransactionDate = DateTime.UtcNow,
                TransactionType = "Withdrawal"
            };

            var response = await _transactionService.AddTransactionAsync(transaction);

            if (response == null)
                return Unauthorized(new { message = "Error registering the transaction." });

            return Ok(response);
           
        }
    }
}
