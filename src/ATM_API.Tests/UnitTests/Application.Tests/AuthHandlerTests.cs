using System.Threading.Tasks;
using Xunit;
using Moq;
using ATM_API.Application.DTOs.Auth;
using ATM_API.Application.Interfaces.Auth;
using ATM_API.Application.Interfaces.Security;
using ATM_API.Application.Interfaces;
using ATM_API.Core.Entities;
using ATM_API.Application.Features.Auth;

namespace ATM_API.Tests.UnitTests.Application.Tests
{
    

    public class AuthFeatureTests
    {
        private readonly Mock<ICardRepository> _cardRepositoryMock = new();
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock = new();
        private readonly Mock<IHashingService> _hashingServiceMock = new();
        private readonly LoginHandler _loginHandler;

        public AuthFeatureTests()
        {
            _loginHandler = new LoginHandler(_cardRepositoryMock.Object, _jwtTokenServiceMock.Object, _hashingServiceMock.Object);
        }

        [Fact]
        public async Task AuthenticateAsync_WithCorrectCredentials_ReturnsToken()
        {
            var card = new Card { CardNumber = "1234567890", PINHash = "hashedPIN", IsBlocked = false };
            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);
            _hashingServiceMock.Setup(x => x.VerifyHash("1234", "hashedPIN")).Returns(true);
            _jwtTokenServiceMock.Setup(x => x.GenerateToken(card)).Returns("validToken");

            var result = await _loginHandler.AuthenticateAsync(new LoginRequestDto { CardNumber = "1234567890", PIN = "1234" });

            Assert.NotNull(result);
            Assert.Equal("validToken", result.Token);
        }

        [Fact]
        public async Task AuthenticateAsync_WithIncorrectPIN_IncrementsFailedAttempts()
        {
            var card = new Card { CardNumber = "1234567890", PINHash = "hashedPIN", IsBlocked = false, FailedAttempts = 0 };
            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);
            _hashingServiceMock.Setup(x => x.VerifyHash("wrongPIN", "hashedPIN")).Returns(false);

            var result = await _loginHandler.AuthenticateAsync(new LoginRequestDto { CardNumber = "1234567890", PIN = "wrongPIN" });

            Assert.Null(result);
            Assert.Equal(1, card.FailedAttempts);
        }

        [Fact]
        public async Task AuthenticateAsync_WithFourFailedAttempts_BlocksCard()
        {
            var card = new Card { CardNumber = "1234567890", PINHash = "hashedPIN", IsBlocked = false, FailedAttempts = 3 };
            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);
            _hashingServiceMock.Setup(x => x.VerifyHash("wrongPIN", "hashedPIN")).Returns(false);

            var result = await _loginHandler.AuthenticateAsync(new LoginRequestDto { CardNumber = "1234567890", PIN = "wrongPIN" });

            Assert.Null(result);
            Assert.True(card.IsBlocked);
        }
    }

}


