using ApiTest.DTOs;
using ApiTest.Model;

namespace ApiTest.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(Guid identifier);
        Task<Account> CreateAsync(AccountDTO account);
        Task<bool> UpdateAsync(Account account);
        Task<bool> DeleteAsync(Guid identifier);
        Task<Account> GetByCnpjAsync(string cnpj);
    }
}