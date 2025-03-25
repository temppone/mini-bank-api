using ApiTest.DTOs;
using ApiTest.Repositories.Interfaces;
using ApiTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionController(ITransactionRepository transactionRepository, TransactionService transactionService) : ControllerBase
    {

        [HttpGet("{accountIdentifier}")]
        public async Task<ActionResult> GetTransactionsByAccountIdentifier(Guid accountIdentifier)
        {
            var transactions = await transactionRepository.GetAllByAccountIdentifierAsync(accountIdentifier);

            return Ok(transactions);
        }

        [HttpPost]
        public async Task<ActionResult> CreateTransaction(TransactionDTO transactionDTO)
        {
            var createdTransaction = await transactionService.CreateTransactionAsync(transactionDTO);

            return CreatedAtAction(nameof(CreateTransaction), new { identifier = createdTransaction.Identifier }, createdTransaction);
        }

        [HttpGet("DailyTransactions/{accountIdentifier}")]
        public async Task<ActionResult> GetDailyTransactionsByAccountIdentifier(Guid accountIdentifier)
        {
            var dailyTransactions = await transactionRepository.GetDailyTransactionsAsync(accountIdentifier);

            return Ok(dailyTransactions);
        }
    }
}