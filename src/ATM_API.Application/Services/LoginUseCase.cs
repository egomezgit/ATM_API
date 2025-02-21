using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Application.UseCases
{
    // ATM_API.Application/UseCases/LoginUseCase.cs
    public class LoginUseCase
    {
        private readonly IUserRepository _userRepository;

        public LoginUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Execute(string cardNumber, string pin)
        {
            var user = await _userRepository.GetUserByCardNumberAsync(cardNumber);
            if (user == null || user.IsBlocked)
                throw new UnauthorizedAccessException("Tarjeta bloqueada o no existe.");

            if (user.Pin != pin)
                throw new UnauthorizedAccessException("PIN incorrecto.");

            return "TokenGenerado"; // Aquí se generaría el JWT
        }
    }
}
