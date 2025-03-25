using ApiTest.DTOs;
using ApiTest.Views;

namespace ApiTest.Services.Domain
{
    public interface ITransactionService
    {
        Task<TransactionView> CreateTransactionAsync(TransactionDTO transactionDTO);
    }
}