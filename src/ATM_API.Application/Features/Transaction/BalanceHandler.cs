
using ATM_API.Application.DTOs;
using ATM_API.Application.Interfaces;
using ATM_API.Application.Interfaces.Operation;
using ATM_API.Core.Entities;

namespace ATM_API.Application.Features.Transaction
{
    public class BalanceHandler : IBalanceService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ITransactionRepository _transactionRepository;

        public BalanceHandler(ICardRepository cardRepository, IAccountRepository accountRepository, ITransactionRepository transactionRepository)
        {
            _cardRepository = cardRepository;
            _accountRepository = accountRepository;
            _transactionRepository = transactionRepository;
        }

        // Método para obtener el saldo actual
        public async Task<BalanceResponseDto> HandleBalanceAsync(string cardNumber)
        {
            var card = await _cardRepository.GetByCardNumberAsync(cardNumber);
            if (card == null || card.IsBlocked)
                return null;

            var account = await _accountRepository.GetAccountWithUserAsync(card.AccountId);
            if (account == null || account.User == null)
                return null;


            var lastTransaction = await _transactionRepository.GetLastTransactionAsync(account.Id);

            return new BalanceResponseDto
            {
                UserName = account.User.Name,
                AccountNumber = account.AccountNumber,
                Balance = account.Balance,
                LastTransactionDate = lastTransaction?.Date
            };
        }

        // Métodos previos de la clase como HandleTransactionAsync, HandleHistoryAsync, etc.
    }
}

