using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_API.Infrastructure.Repositories
{
    // ATM_API.Infrastructure/Repositories/UserRepository.cs
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByCardNumberAsync(string cardNumber)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.CardNumber == cardNumber);
        }
    }
}
