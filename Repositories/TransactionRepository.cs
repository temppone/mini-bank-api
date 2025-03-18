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

        public Task<List<Transaction>> GetAllAsync(Guid accountIdentifier)
        {
            var transactions = _transactionDbSet.ToList().Where(t =>
                t.SourceAccountIdentifier == accountIdentifier || t.DestinationAccountIdentifier == accountIdentifier).ToList();

            return Task.FromResult(new List<Transaction>());
        }


        public Task<Transaction?> GetByIdAsync(Guid identifier)
        {
            var transaction = _transactionDbSet.ToList().FirstOrDefault(t => t.Identifier == identifier);

            return Task.FromResult(transaction);
        }

        public Task<Transaction> CreateAsync(TransactionDTO transaction)
        {
            var newTransaction = new Transaction
            {
                Amount = transaction.Amount,
                Date = transaction.Date,
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