
using Microsoft.AspNetCore.Mvc;
using ATM_API.Application.Interfaces.Auth;
using ATM_API.Application.DTOs.Auth;

namespace ATM_API.Web.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginDto)
        {
            var response = await _authService.AuthenticateAsync(loginDto);
            if (response == null)
                return Unauthorized(new { message = "Invalid credentials or card is blocked." });

            return Ok(response);
        }
    }
}

