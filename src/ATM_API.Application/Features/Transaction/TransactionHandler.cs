

using ATM_API.Application.DTOs.Transaction;
using ATM_API.Application.Interfaces;
using ATM_API.Application.Interfaces.Operation;

namespace ATM_API.Application.Features.Transaction
{
    public class TransactionHandler : ITransactionService
    {
        private readonly ICardRepository _cardRepository;
        private readonly IAccountRepository _accountRepository;

        public TransactionHandler(ICardRepository cardRepository, IAccountRepository accountRepository)
        {
            _cardRepository = cardRepository;
            _accountRepository = accountRepository;
        }

        public async Task<TransactionResponseDto> HandleAsync(TransactionRequestDto request)
        {
            var card = await _cardRepository.GetByCardNumberAsync(request.CardNumber);
            if (card == null || card.IsBlocked)
                return new TransactionResponseDto { Success = false, Message = "Card not found or blocked." };

            var account = await _accountRepository.GetByIdAsync(card.AccountId);
            if (account == null)
                return new TransactionResponseDto { Success = false, Message = "Account not found." };

            if (account.Balance < request.Amount)
                return new TransactionResponseDto { Success = false, Message = "Insufficient balance." };

            account.Balance -= request.Amount;
            await _accountRepository.UpdateAsync(account);

            return new TransactionResponseDto
            {
                Success = true,
                NewBalance = account.Balance,
                Message = "Transaction successful."
            };
        }
    }
}


