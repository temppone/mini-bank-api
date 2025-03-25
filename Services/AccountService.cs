using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using ApiTest.Services.Domain;
using ApiTest.Utils;
using ApiTest.Views;
using Microsoft.Extensions.Caching.Memory;

namespace ApiTest.Services
{
    public class AccountService(
        IAccountRepository accountRepository,
        IBalanceRepository balanceRepository,
        CNPJService CNPJService,
        ApplicationDbContext context,
        IMemoryCache cache) : IAccountService
    {
        public async Task<AccountView> CreateAccountAsync(string cnpj)
        {
            using var transaction = await context.Database.BeginTransactionAsync();

            try
            {
                if (cache.TryGetValue(cnpj, out AccountView? cachedData) && cachedData != null)
                {
                    return cachedData;
                }

                bool isValidCnpj = CnpjValidator.IsValidCnpj(cnpj);

                if (!isValidCnpj)
                {
                    throw new InvalidOperationException("Invalid CNPJ");
                }


                var isCompanyFound = await accountRepository.GetByCnpjAsync(cnpj);

                if (isCompanyFound != null)
                {
                    throw new InvalidOperationException("Account with this CNPJ already exists.");
                }

                var cnpjData = await CNPJService.GetCompanyData(cnpj);

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

                var createdAccount = (await accountRepository.CreateAsync(account) ?? throw new Exception("Failed to create account")) ?? throw new Exception("Failed to create account");

                var balance = new BalanceDTO
                {
                    Amount = 100,
                    AccountIdentifier = createdAccount.Identifier,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                AccountView accountView = new()
                {
                    Cnpj = createdAccount.Cnpj,
                    Identifier = createdAccount.Identifier,
                    Name = createdAccount.Name,
                };

                await balanceRepository.CreateAsync(balance);

                await context.SaveChangesAsync();

                await transaction.CommitAsync();

                cache.Set(cnpj, createdAccount, TimeSpan.FromHours(1));

                return accountView;
            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}