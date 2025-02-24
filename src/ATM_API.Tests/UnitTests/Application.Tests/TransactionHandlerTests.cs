using ATM_API.Application.DTOs.Transaction;
using ATM_API.Application.Features.Transaction;
using ATM_API.Application.Interfaces;
using ATM_API.Core.Entities;
using Moq;


namespace ATM_API.Tests.UnitTests.Application.Tests
{
    public class TransactionHandlerTests
    {
        private readonly Mock<ICardRepository> _cardRepositoryMock = new();
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock = new();
        private readonly TransactionHandler _transactionHandler;

        public TransactionHandlerTests()
        {
            _transactionHandler = new TransactionHandler(_cardRepositoryMock.Object, _accountRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleTransactionAsync_WithSufficientBalance_ReturnsSuccess()
        {
            var card = new Card { CardNumber = "1234567890", IsBlocked = false, AccountId = 1 };
            var account = new Account { Id = 1, Balance = 500 };

            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);
            _accountRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(account);

            var result = await _transactionHandler.HandleAsync(new TransactionRequestDto { CardNumber = "1234567890", Amount = 200 });

            Assert.True(result.Success);
            Assert.Equal(300, account.Balance);
        }

        [Fact]
        public async Task HandleTransactionAsync_WithInsufficientBalance_ReturnsFailure()
        {
            var card = new Card { CardNumber = "1234567890", IsBlocked = false, AccountId = 1 };
            var account = new Account { Id = 1, Balance = 100 };

            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);
            _accountRepositoryMock.Setup(x => x.GetByIdAsync(1)).ReturnsAsync(account);

            var result = await _transactionHandler.HandleAsync(new TransactionRequestDto { CardNumber = "1234567890", Amount = 200 });

            Assert.False(result.Success);
            Assert.Equal("Insufficient balance.", result.Message);
        }
    }

}
