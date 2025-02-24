
using ATM_API.Application.DTOs.Transaction;
using ATM_API.Application.Interfaces;
using ATM_API.Application.Interfaces.Operation;


namespace ATM_API.Application.Features.Transaction
{
    public class TransactionHistoryHandler : ITransactionHistoryService
    {
        private readonly ICardRepository _cardRepository;
        private readonly ITransactionRepository _transactionRepository;

        public TransactionHistoryHandler(ICardRepository cardRepository, ITransactionRepository transactionRepository)
        {
            _cardRepository = cardRepository;
            _transactionRepository = transactionRepository;
        }

        public async Task<TransactionHistoryResponseDto> HandleAsync(TransactionHistoryRequestDto request)
        {
            var card = await _cardRepository.GetByCardNumberAsync(request.CardNumber);
            if (card == null || card.IsBlocked)
                return null;

            int pageSize = 10;
            var totalRecords = await _transactionRepository.GetTotalTransactionsAsync(card.AccountId);
            var transactions = await _transactionRepository.GetByAccountIdAsync(card.AccountId, request.Page, pageSize);

            return new TransactionHistoryResponseDto
            {
                TotalRecords = totalRecords,
                CurrentPage = request.Page,
                TotalPages = (int)System.Math.Ceiling((double)totalRecords / pageSize),
                Transactions = transactions.Select(t => new TransactionDto { Amount = t.Amount, TransactionDate = t.Date }).ToList()
            };
        }
    }
}

