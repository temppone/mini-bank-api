using ApiTest.DTOs;
using ApiTest.Model;

namespace ApiTest.Repositories.Interfaces
{
    public interface IBalanceRepository
    {
        Task<Balance?> GetByAccountIdAsync(Guid id);
        Task<Balance> CreateAsync(BalanceDTO balance);
        Task<bool> UpdateAsync(Balance balance);
    }
}