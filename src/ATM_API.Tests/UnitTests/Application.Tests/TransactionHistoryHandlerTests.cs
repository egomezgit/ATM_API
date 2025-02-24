using ATM_API.Application.DTOs.Transaction;
using ATM_API.Application.Interfaces;
using ATM_API.Core.Entities;
using Moq;
using ATM_API.Application.Features.Transaction;


namespace ATM_API.Tests.UnitTests.Application.Tests
{
    public class TransactionHistoryHandlerTests
    {
        private readonly Mock<ICardRepository> _cardRepositoryMock = new();
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock = new();
        private readonly TransactionHistoryHandler _transactionHistoryHandler;

        public TransactionHistoryHandlerTests()
        {
            _transactionHistoryHandler = new TransactionHistoryHandler(_cardRepositoryMock.Object, _transactionRepositoryMock.Object);
        }

        [Fact]
        public async Task HandleHistoryAsync_WithValidCard_ReturnsTransactions()
        {
            var card = new Card { CardNumber = "1234567890", IsBlocked = false, AccountId = 1 };
            var transactions = new List<Transaction>
        {
            new Transaction { AccountId = 1, Amount = 100, Date = System.DateTime.UtcNow },
            new Transaction { AccountId = 1, Amount = -50, Date = System.DateTime.UtcNow.AddDays(-1) }
        };

            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);
            _transactionRepositoryMock.Setup(x => x.GetByAccountIdAsync(1, 1, 10)).ReturnsAsync(transactions);
            _transactionRepositoryMock.Setup(x => x.GetTotalTransactionsAsync(1)).ReturnsAsync(2);

            var result = await _transactionHistoryHandler.HandleAsync(new TransactionHistoryRequestDto { CardNumber = "1234567890", Page = 1 });

            Assert.NotNull(result);
            Assert.Equal(2, result.Transactions.Count);
            Assert.Equal(1, result.CurrentPage);
            Assert.Equal(1, result.TotalPages);
        }

        [Fact]
        public async Task HandleHistoryAsync_WithBlockedCard_ReturnsNull()
        {
            var card = new Card { CardNumber = "1234567890", IsBlocked = true };

            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("1234567890")).ReturnsAsync(card);

            var result = await _transactionHistoryHandler.HandleAsync(new TransactionHistoryRequestDto { CardNumber = "1234567890", Page = 1 });

            Assert.Null(result);
        }

        [Fact]
        public async Task HandleHistoryAsync_WithNonexistentCard_ReturnsNull()
        {
            _cardRepositoryMock.Setup(x => x.GetByCardNumberAsync("nonexistent")).ReturnsAsync((Card)null);

            var result = await _transactionHistoryHandler.HandleAsync(new TransactionHistoryRequestDto { CardNumber = "nonexistent", Page = 1 });

            Assert.Null(result);
        }
    }

}
