using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        private readonly DbSet<Transaction> _transactionDbSet;

        public TransactionRepository(ApplicationDbContext context)
        {
            _transactionDbSet = context.Transactions;
        }

        public async Task<List<Transaction>> GetAllByAccountIdentifierAsync(Guid accountIdentifier)
        {
            return await _transactionDbSet
                .Where(t => t.SourceAccountIdentifier == accountIdentifier || t.DestinationAccountIdentifier == accountIdentifier)
                .ToListAsync();
        }


        public Task<Transaction?> GetByIdAsync(Guid identifier)
        {
            var transactions = _transactionDbSet.ToList().FirstOrDefault(t => t.Identifier == identifier);

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

            _transactionDbSet.Add(newTransaction);

            return Task.FromResult(newTransaction);
        }

        public Task<List<DailyTransactions>> GetDailyTransactionsAsync(Guid accountId)
        {
            var transactions = _transactionDbSet.ToList().Where(t =>
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