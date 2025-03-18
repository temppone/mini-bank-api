
using ApiTest.DTOs;
using ApiTest.Model;

namespace ApiTest.Repositories.Interfaces
{
    public interface ITransactionRepository
    {
        Task<List<Transaction>> GetAllAsync(Guid identifier);
        Task<Transaction?> GetByIdAsync(Guid identifier);
        Task<Transaction> CreateAsync(TransactionDTO transaction);
        Task<List<DailyTransactions>> GetDailyTransactionsAsync(Guid accountId);
    }
}