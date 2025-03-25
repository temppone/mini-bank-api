using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using ApiTest.Views;

namespace ApiTest.Repositories
{
    public class AccountRepository(ApplicationDbContext dbContext) : IAccountRepository
    {
        public Task<List<Account>> GetAllAsync()
        {
            return Task.FromResult(dbContext.Accounts.ToList());
        }

        public Task<Account?> GetByIdAsync(Guid id)
        {
            var account = dbContext.Accounts.ToList().FirstOrDefault(a => a.Identifier == id);

            return Task.FromResult(account);
        }


        public Task<Account?> GetByCnpjAsync(string cnpj)
        {
            var account = dbContext.Accounts.ToList().FirstOrDefault(a => a.Cnpj == cnpj);

            return Task.FromResult(account);
        }


        public Task<Account> CreateAsync(AccountDTO account)
        {
            var newAccount = new Account
            {
                Identifier = Guid.NewGuid(),
                Name = account.Name,
                Cnpj = account.Cnpj,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            dbContext.Accounts.Add(newAccount);


            AccountView accountVIew = new()
            {
                Cnpj = newAccount.Cnpj,
                Identifier = newAccount.Identifier,
                Name = newAccount.Name
            };


            return Task.FromResult(newAccount);
        }

        public Task<bool> UpdateAsync(Account account)
        {
            var existingAccount = dbContext.Accounts.ToList().FirstOrDefault(a => a.Identifier == account.Identifier);
            if (existingAccount != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var account = dbContext.Accounts.ToList().FirstOrDefault(a => a.Identifier == id);
            if (account != null)
            {
                dbContext.Accounts.Remove(account);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}