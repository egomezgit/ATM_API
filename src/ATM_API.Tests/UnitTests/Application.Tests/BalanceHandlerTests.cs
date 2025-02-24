using ATM_API.Application.Interfaces;
using ATM_API.Core.Entities;
using Moq;
using ATM_API.Application.Features.Transaction;

namespace ATM_API.Tests.UnitTests.Application.Tests
{
  public class BalanceHandlerTests
    {
        private readonly Mock<ICardRepository> _cardRepositoryMock = new();
        private readonly Mock<IAccountRepository> _accountRepositoryMock = new();
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock = new();
        private readonly BalanceHandler _balanceHandler;

        public BalanceHandlerTests()
        {
            _balanceHandler = new BalanceHandler(_cardRepositoryMock.Object, _accountRepositoryMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleBalanceAsync_WithValidCard_ReturnsBalance()
        {
            var card = new Card { CardNumber = "1234567890", IsBlocked = false, AccountId = 1 };
            var account = new Account { Id = 1, Balance = 500, AccountNumber = "ACC123456", User = new User { Name = "John Doe" } };

            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);
            _accountRepositoryMock.Setup(x => x.GetAccountWithUserAsync(1)).ReturnsAsync(account);

            var result = await _balanceHandler.HandleBalanceAsync("1234567890");

            Assert.NotNull(result);
            Assert.Equal("John Doe", result.UserName);
            Assert.Equal("ACC123456", result.AccountNumber);
            Assert.Equal(500, result.Balance);
        }

        [Fact]
        public async Task HandleBalanceAsync_WithBlockedCard_ReturnsNull()
        {
            var card = new Card { CardNumber = "1234567890", IsBlocked = true };

            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);

            var result = await _balanceHandler.HandleBalanceAsync("1234567890");

            Assert.Null(result);
        }

        [Fact]
        public async Task HandleBalanceAsync_WithNonexistentCard_ReturnsNull()
        {
            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("nonexistent")).ReturnsAsync((Card)null);

            var result = await _balanceHandler.HandleBalanceAsync("nonexistent");

            Assert.Null(result);
        }
    }

}
