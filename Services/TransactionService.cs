using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Services
{
    public class TransactionService(
        IAccountRepository accountRepository,
        IBalanceRepository balanceRepository,
        ITransactionRepository transactionRepository,
        ApplicationDbContext context
        )
    {

        private readonly ITransactionRepository _transactionRepository = transactionRepository;
        private readonly IAccountRepository _accountRepository = accountRepository;
        private readonly IBalanceRepository _balanceRepository = balanceRepository;
        private readonly ApplicationDbContext _dbcontext = context;

        public async Task<Transaction> CreateTransaction(TransactionDTO transactionDTO)
        {

            using var dbTransaction = await _dbcontext.Database.BeginTransactionAsync();

            try
            {
                var sourceAccountBalance = await _balanceRepository
                    .GetByAccountIdAsync(transactionDTO.SourceAccountIdentifier) ?? throw new Exception("Source account balance not found");

                if (sourceAccountBalance.Amount < transactionDTO.Amount)
                {
                    throw new Exception("Insuficient amount");
                }

                var destinationAccountBalance = await _balanceRepository
                    .GetByAccountIdAsync(transactionDTO.DestinationAccountIdentifier) ?? throw new Exception("Destination account balance not found");

                sourceAccountBalance.Withdraw(transactionDTO.Amount);
                destinationAccountBalance.Deposit(transactionDTO.Amount);

                await _balanceRepository.UpdateAsync(sourceAccountBalance);
                await _balanceRepository.UpdateAsync(destinationAccountBalance);

                TransactionDTO sourceAccountTransaction = new()
                {
                    Amount = transactionDTO.Amount,
                    DestinationAccountIdentifier = transactionDTO.SourceAccountIdentifier,
                    SourceAccountIdentifier = transactionDTO.DestinationAccountIdentifier
                };

                TransactionDTO destinationAccountTransaction = new()
                {
                    Amount = transactionDTO.Amount,
                    DestinationAccountIdentifier = transactionDTO.DestinationAccountIdentifier,
                    SourceAccountIdentifier = transactionDTO.SourceAccountIdentifier
                };

                Transaction createdTransaction = await _transactionRepository.CreateAsync(sourceAccountTransaction);
                await _transactionRepository.CreateAsync(destinationAccountTransaction);

                await _dbcontext.SaveChangesAsync();

                await dbTransaction.CommitAsync();

                return createdTransaction;
            }
            catch
            {
                await dbTransaction.RollbackAsync();

                throw;
            }
        }
    }
}