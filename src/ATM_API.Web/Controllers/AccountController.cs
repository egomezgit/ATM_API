using ATM_API.Application.Interfaces;
using ATM_API.Application.Interfaces.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ATM_API.Web.Controllers
{


    // Controllers/AccountController.cs
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ICardRepository _cardRepository;

        public AccountController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("balance/{cardNumber}")]
        public async Task<IActionResult> GetBalance(string cardNumber)
        {
            var user = await _userRepository.GetUserByCardNumberAsync(cardNumber);
            if (user == null)
                return NotFound("Usuario no encontrado.");

            return Ok(new
            {
                user.Name,
                user.AccountNumber,
                user.Balance,
                user.LastWithdrawalDate
            });
        }
    }

}