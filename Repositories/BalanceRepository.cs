using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repositories
{
    public class BalanceRepository(ApplicationDbContext dbContext) : IBalanceRepository
    {
        public Task<Balance?> GetByAccountIdAsync(Guid accountIdentifier)
        {
            var balance = dbContext.Balances.ToList().FirstOrDefault(b => b.AccountIdentifier == accountIdentifier);

            return Task.FromResult(balance);
        }

        public Task<Balance> CreateAsync(BalanceDTO balance)
        {
            var newBalance = new Balance
            {
                Identifier = Guid.NewGuid(),
                Amount = balance.Amount,
                AccountIdentifier = balance.AccountIdentifier,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            dbContext.Balances.Add(newBalance);

            return Task.FromResult(newBalance);
        }

        public async Task<bool> UpdateAsync(Balance balance)
        {
            var existingBalance = await dbContext.Balances
                .FirstOrDefaultAsync(b => b.Identifier == balance.Identifier);

            if (existingBalance != null)
            {
                existingBalance.Amount = balance.Amount;
                existingBalance.UpdatedAt = DateTime.UtcNow;

                return true;
            }

            return false;
        }
    }
}