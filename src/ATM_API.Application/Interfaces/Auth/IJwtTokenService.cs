using ATM_API.Core.Entities;

namespace ATM_API.Application.Interfaces.Auth
{
    public interface IJwtTokenService
    {
        string GenerateToken(Card card);
    }
}
