
using ATM_API.Application.Interfaces;
using ATM_API.Application.Interfaces.Security;
using ATM_API.Core.Entities;
using ATM_API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;


namespace ATM_API.Infrastructure.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;
        private readonly IHashingService _hashingService;

        public CardRepository(AppDbContext context,
            IHashingService hashingService)
        {
            _context = context;
            _hashingService = hashingService;
        }

        public async Task<Card?> GetByCardNumberAsync(string cardNumber)
        {
            return await _context.Cards.FirstOrDefaultAsync(c => c.CardNumber == cardNumber);
        }

        public async Task UpdateAsync(Card card)
        {
            _context.Cards.Update(card);
            await _context.SaveChangesAsync();
        }

        public async Task CreateCardAsync(Card card, string plainTextPIN)
        {
            card.PINHash = _hashingService.Hash(plainTextPIN); // Hashear el PIN antes de almacenarlo
            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
        }


    }
}