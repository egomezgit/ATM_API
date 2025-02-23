using ATM_API.Application.DTOs.Auth;
using ATM_API.Application.Features.Auth;
using ATM_API.Application.Interfaces.Auth;
using Moq;

using Xunit;
using Moq;
using System.Threading.Tasks;
using CleanArchitectureApi.Application.Features.Auth;
using CleanArchitectureApi.Application.Interfaces;
using CleanArchitectureApi.Domain.Entities;
using CleanArchitectureApi.Infrastructure.Security;
using System;
using ATM_API.Core.Entities;

namespace ATM_API.Tests.UnitTests.Application.Tests.Features.Auth
{

    public class LoginTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly Mock<ICardRepository> _cardRepositoryMock;
        private readonly Mock<IJwtTokenService> _jwtTokenServiceMock;
        private readonly LoginHandler _loginHandler;


        public LoginTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _cardRepositoryMock = new Mock<ICardRepository>();
            _jwtTokenServiceMock = new Mock<IJwtTokenService>();
            _loginHandler = new LoginHandler(_cardRepositoryMock.Object, _jwtTokenServiceMock.Object);
        }
     

        [Fact]
        public async Task Login_Success_ReturnsJwtToken()
        {
            // Arrange
            var request = new LoginRequestDto { CardNumber = "123456789", PIN = "1234" };
            var card = new Card { CardNumber = request.CardNumber, PINHash = request.PIN };
            _cardRepositoryMock.Setup(r => r.GetCardByNumberAsync(request.CardNumber))
                .ReturnsAsync(card);
            _jwtTokenServiceMock.Setup(t => t.GenerateToken(It.IsAny<Card>()))
                .Returns("mocked-jwt-token");

            // Act
            var result = await _loginHandler.Handle(request, default);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("mocked-jwt-token", result);
        }

        [Fact]
        public async Task Login_IncorrectPin_FailsAfterFourAttempts()
        {
            var request = new LoginCommand { CardNumber = "123456789", Pin = "wrong" };
            _authServiceMock.SetupSequence(s => s.AuthenticateAsync(request.CardNumber, request.Pin))
                .ThrowsAsync(new UnauthorizedAccessException())
                .ThrowsAsync(new UnauthorizedAccessException())
                .ThrowsAsync(new UnauthorizedAccessException())
                .ThrowsAsync(new UnauthorizedAccessException())
                .ThrowsAsync(new Exception("Card Blocked"));

            for (int i = 0; i < 4; i++)
            {
                await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _loginHandler.Handle(request, default));
            }

            var ex = await Assert.ThrowsAsync<Exception>(() => _loginHandler.Handle(request, default));
            Assert.Equal("Card Blocked", ex.Message);
        }

        [Fact]
        public async Task Login_NonExistentCard_ReturnsError()
        {
            var request = new LoginCommand { CardNumber = "000000000", Pin = "1234" };
            _authServiceMock.Setup(s => s.AuthenticateAsync(request.CardNumber, request.Pin))
                .ThrowsAsync(new KeyNotFoundException("Card not found"));

            var ex = await Assert.ThrowsAsync<KeyNotFoundException>(() => _loginHandler.Handle(request, default));
            Assert.Equal("Card not found", ex.Message);
        }
    }


}
