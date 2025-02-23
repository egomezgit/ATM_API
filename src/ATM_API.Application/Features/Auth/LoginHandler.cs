using ATM_API.Application.DTOs.Auth;
using ATM_API.Application.Interfaces.Auth;



namespace ATM_API.Application.Features.Auth
{
    public class LoginHandler : IAuthService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginHandler(ICardRepository cardRepository, IJwtTokenService jwtTokenService)
        {
            _cardRepository = cardRepository;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponseDto?> AuthenticateAsync(LoginRequestDto request)
        {
            var card = await _cardRepository.GetByCardNumberAsync(request.CardNumber);
            if (card == null || card.IsBlocked)
                return null;

            if (card.PINHash != request.PIN) // En producción usar hashing
            {
                card.FailedAttempts++;
                if (card.FailedAttempts >= 4)
                    card.IsBlocked = true;

                await _cardRepository.UpdateAsync(card);
                return null;
            }

            card.FailedAttempts = 0;
            await _cardRepository.UpdateAsync(card);

            return new LoginResponseDto { Token = _jwtTokenService.GenerateToken(card) };
        }
    }
}

