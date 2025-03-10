﻿using ATM_API.Application.Interfaces.Security;
using ATM_API.Core.Entities;
using ATM_API.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace ATM_API.Infrastructure.Seeds
{
    public static class DefaultData
    {
        public static async Task SeedAsync(AppDbContext context, IHashingService hashingService)
        {
            if (!await context.Users.AnyAsync())
            {
                // Crear usuarios
                var user1 = new User { Name = "John Doe" };
                var user2 = new User { Name = "Jane Smith" };
                var user3 = new User { Name = "Alice Johnson" };
                var user4 = new User { Name = "Bob Brown" };

                context.Users.AddRange(user1, user2, user3, user4);
                await context.SaveChangesAsync();

                // Crear cuentas asociadas a los usuarios
                var account1 = new Account { AccountNumber = "ACC123456", Balance = 1000.00m, UserId = user1.Id };
                var account2 = new Account { AccountNumber = "ACC789012", Balance = 500.00m, UserId = user2.Id };
                var account3 = new Account { AccountNumber = "ACC345678", Balance = 2000.00m, UserId = user3.Id };
                var account4 = new Account { AccountNumber = "ACC901234", Balance = 1500.00m, UserId = user4.Id };

                context.Accounts.AddRange(account1, account2, account3, account4);
                await context.SaveChangesAsync();

                // Crear tarjetas asociadas a las cuentas
                var card1 = new Card
                {
                    CardNumber = "1234567890",
                    PINHash = hashingService.Hash("1234"),
                    IsBlocked = false,
                    FailedAttempts = 0,
                    AccountId = account1.Id
                };

                var card2 = new Card
                {
                    CardNumber = "9876543210",
                    PINHash = hashingService.Hash("5678"),
                    IsBlocked = false,
                    FailedAttempts = 0,
                    AccountId = account2.Id
                };

                var card3 = new Card
                {
                    CardNumber = "1111222233334444",
                    PINHash = hashingService.Hash("9999"),
                    IsBlocked = false,
                    FailedAttempts = 0,
                    AccountId = account3.Id
                };

                var card4 = new Card
                {
                    CardNumber = "5555666677778888",
                    PINHash = hashingService.Hash("0000"),
                    IsBlocked = false,
                    FailedAttempts = 0,
                    AccountId = account4.Id
                };

                context.Cards.AddRange(card1, card2, card3, card4);
                await context.SaveChangesAsync();

                // Crear transacciones asociadas a las cuentas
                var transactions = new List<Transaction>();

                for (int i = 1; i <= 20; i++)
                {
                    transactions.Add(new Transaction { AccountId = account1.Id, Amount = i % 2 == 0 ? i * 10 : i * -10, Date = DateTime.UtcNow.AddDays(-i) });
                    transactions.Add(new Transaction { AccountId = account2.Id, Amount = i % 2 == 0 ? i * 5 : i * -5, Date = DateTime.UtcNow.AddDays(-i) });
                    transactions.Add(new Transaction { AccountId = account3.Id, Amount = i % 2 == 0 ? i * 15 : i * -15, Date = DateTime.UtcNow.AddDays(-i) });
                    transactions.Add(new Transaction { AccountId = account4.Id, Amount = i % 2 == 0 ? i * 20 : i * -20, Date = DateTime.UtcNow.AddDays(-i) });
                }

                context.Transactions.AddRange(transactions);
                await context.SaveChangesAsync();
            }
        }
    }
}

