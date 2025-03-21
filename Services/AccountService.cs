using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using ApiTest.Utils;
using Microsoft.Extensions.Caching.Memory;

namespace ApiTest.Services
{
    public class AccountService(IAccountRepository accountRepository, IBalanceRepository balanceRepository, CNPJService CNPJService, ApplicationDbContext context, IMemoryCache cache)
    {
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IBalanceRepository _balanceRepository = balanceRepository;
        private readonly CNPJService _CNPJService = CNPJService;
        private readonly ApplicationDbContext _dbContext = context;
        private readonly IMemoryCache _cache = cache;

        public async Task<Account> CreateAccountAsync(string cnpj)
        {
            using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                if (_cache.TryGetValue(cnpj, out Account? cachedData) && cachedData != null)
                {
                    return cachedData;
                }

                bool isValidCnpj = CnpjValidator.IsValidCnpj(cnpj);

                if (!isValidCnpj)
                {
                    throw new InvalidOperationException("Invalid CNPJ");
                }


                var isCompanyFound = await _accountRepository.GetByCnpjAsync(cnpj);

                if (isCompanyFound != null)
                {
                    throw new InvalidOperationException("Account with this CNPJ already exists.");
                }

                var cnpjData = await _CNPJService.GetCompanyData(cnpj);

                if (cnpjData.Situacao != "ATIVA")
                {
                    throw new Exception("Account status invalid");
                }

                var account = new AccountDTO
                {
                    Cnpj = cnpj,
                    Name = cnpjData.Nome,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                var createdAccount = (await _accountRepository.CreateAsync(account) ?? throw new Exception("Failed to create account")) ?? throw new Exception("Failed to create account");

                var balance = new BalanceDTO
                {
                    Amount = 100,
                    AccountIdentifier = createdAccount.Identifier,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _balanceRepository.CreateAsync(balance);

                await _dbContext.SaveChangesAsync();

                await transaction.CommitAsync();

                _cache.Set(cnpj, createdAccount, TimeSpan.FromHours(1));

                return createdAccount;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}