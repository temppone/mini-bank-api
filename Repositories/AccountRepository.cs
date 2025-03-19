using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;

namespace ApiTest.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public Task<List<Account>> GetAllAsync()
        {
            return Task.FromResult(_dbContext.Accounts.ToList());
        }

        public Task<Account?> GetByIdAsync(Guid id)
        {
            var account = _dbContext.Accounts.ToList().FirstOrDefault(a => a.Identifier == id);
            return Task.FromResult(account);
        }


        public Task<Account?> GetByCnpjAsync(string cnpj)
        {
            var account = _dbContext.Accounts.ToList().FirstOrDefault(a => a.Cnpj == cnpj);

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

            _dbContext.Accounts.Add(newAccount);

            return Task.FromResult(newAccount);
        }

        public Task<bool> UpdateAsync(Account account)
        {
            var existingAccount = _dbContext.Accounts.ToList().FirstOrDefault(a => a.Identifier == account.Identifier);
            if (existingAccount != null)
            {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var account = _dbContext.Accounts.ToList().FirstOrDefault(a => a.Identifier == id);
            if (account != null)
            {
                _dbContext.Accounts.Remove(account);
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }
    }
}