
using ATM_API.Application.DTOs.Auth;

namespace ATM_API.Application.Interfaces.Auth
{
    public interface IAuthService
    {
        Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request);
    }
}


