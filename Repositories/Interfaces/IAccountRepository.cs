using ApiTest.DTOs;
using ApiTest.Model;

namespace ApiTest.Repositories.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<Account>> GetAllAsync();
        Task<Account?> GetByIdAsync(Guid id);
        Task<Account> CreateAsync(AccountDTO account);
        Task<bool> UpdateAsync(Account account);
        Task<bool> DeleteAsync(Guid id);
        Task<Account> GetByCnpjAsync(string cnpj);
    }
}