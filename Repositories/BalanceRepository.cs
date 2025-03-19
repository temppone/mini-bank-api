using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repositories
{
    public class BalanceRepository : IBalanceRepository
    {

        private readonly DbSet<Balance> _balanceDbSet;

        public BalanceRepository(ApplicationDbContext context)
        {
            _balanceDbSet = context.Balances;
        }

        public Task<Balance?> GetByAccountIdAsync(Guid accountIdentifier)
        {
            var balance = _balanceDbSet.ToList().FirstOrDefault(b => b.AccountIdentifier == accountIdentifier);

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

            _balanceDbSet.Add(newBalance);

            return Task.FromResult(newBalance);
        }

        public async Task<bool> UpdateAsync(Balance balance)
        {
            var existingBalance = await _balanceDbSet
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