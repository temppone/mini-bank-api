using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repositories
{
    public class TransactionRepository(ApplicationDbContext dbContext) : ITransactionRepository
    {
        public async Task<List<Transaction>> GetAllByAccountIdentifierAsync(Guid accountIdentifier)
        {
            return await dbContext.Transactions
                .Where(t => t.SourceAccountIdentifier == accountIdentifier || t.DestinationAccountIdentifier == accountIdentifier)
                .ToListAsync();
        }


        public Task<Transaction?> GetByIdAsync(Guid identifier)
        {
            var transactions = dbContext.Transactions.ToList().FirstOrDefault(t => t.Identifier == identifier);

            return Task.FromResult(transactions);
        }

        public Task<Transaction> CreateAsync(TransactionDTO transaction)
        {
            var newTransaction = new Transaction
            {
                Amount = transaction.Amount,
                Date = DateTime.UtcNow,
                DestinationAccountIdentifier = transaction.DestinationAccountIdentifier,
                SourceAccountIdentifier = transaction.SourceAccountIdentifier,
                Identifier = Guid.NewGuid(),
            };

            dbContext.Transactions.Add(newTransaction);

            return Task.FromResult(newTransaction);
        }

        public Task<List<DailyTransactions>> GetDailyTransactionsAsync(Guid accountId)
        {
            var transactions = dbContext.Transactions.ToList().Where(t =>
                t.SourceAccountIdentifier == accountId || t.DestinationAccountIdentifier == accountId).ToList();

            var dailyTransactions = transactions.GroupBy(t => t.Date.Date)
                .Select(g => new DailyTransactions
                {
                    Identifier = Guid.NewGuid(),
                    Amount = g.Sum(t => t.Amount),
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Date = g.Key,
                    Transactions = transactions
                }).ToList();

            return Task.FromResult(dailyTransactions);
        }
    }
}