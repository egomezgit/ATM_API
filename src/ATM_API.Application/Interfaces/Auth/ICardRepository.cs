﻿using ATM_API.Core.Entities;

namespace ATM_API.Application.Interfaces.Auth
{
    public interface ICardRepository
    {
        Task<Card?> GetByCardNumberAsync(string cardNumber);
        Task UpdateAsync(Card card);
    }
}

