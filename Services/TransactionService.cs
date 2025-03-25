using ApiTest.DTOs;
using ApiTest.Model;
using ApiTest.Repositories.Interfaces;
using ApiTest.Services.Domain;
using ApiTest.Views;
using Microsoft.EntityFrameworkCore;

namespace ApiTest.Services
{
    public class TransactionService(
        IBalanceRepository balanceRepository,
        ITransactionRepository transactionRepository,
        ApplicationDbContext dbContext
        ) : ITransactionService
    {

        public async Task<TransactionView> CreateTransactionAsync(TransactionDTO transactionDTO)
        {

            using var dbTransaction = await dbContext.Database.BeginTransactionAsync();

            try
            {
                var sourceAccountBalance = await balanceRepository
                    .GetByAccountIdAsync(transactionDTO.SourceAccountIdentifier) ?? throw new Exception("Source account balance not found");

                if (sourceAccountBalance.Amount < transactionDTO.Amount)
                {
                    throw new Exception("Insuficient amount");
                }

                var destinationAccountBalance = await balanceRepository
                    .GetByAccountIdAsync(transactionDTO.DestinationAccountIdentifier) ?? throw new Exception("Destination account balance not found");

                sourceAccountBalance.Withdraw(transactionDTO.Amount);
                destinationAccountBalance.Deposit(transactionDTO.Amount);

                await balanceRepository.UpdateAsync(sourceAccountBalance);
                await balanceRepository.UpdateAsync(destinationAccountBalance);

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

                Transaction createdTransaction = await transactionRepository.CreateAsync(sourceAccountTransaction);
                await transactionRepository.CreateAsync(destinationAccountTransaction);

                TransactionView transactionView = new()
                {
                    Identifier = createdTransaction.Identifier,
                    Amount = createdTransaction.Amount,
                    Date = createdTransaction.Date,
                    SourceAccountIdentifier = createdTransaction.SourceAccountIdentifier,
                    DestinationAccountIdentifier = createdTransaction.DestinationAccountIdentifier,
                };

                await dbContext.SaveChangesAsync();

                await dbTransaction.CommitAsync();

                return transactionView;
            }
            catch
            {
                await dbTransaction.RollbackAsync();

                throw;
            }
        }
    }
}