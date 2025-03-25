using ApiTest.DTOs;
using ApiTest.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BalanceController(IBalanceRepository balanceRepository) : ControllerBase
    {

        [HttpGet("{accountIdentifier}")]
        public async Task<ActionResult> GetBalanceByAccountId(Guid accountIdentifier)
        {
            var balance = await balanceRepository.GetByAccountIdAsync(accountIdentifier);

            if (balance == null)
            {
                return NotFound();
            }

            return Ok(balance);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBalance(BalanceDTO balanceDTO)
        {
            if (balanceDTO?.Amount is null)
            {
                return BadRequest("Amount cannot be null");
            }

            var balance = await balanceRepository.GetByAccountIdAsync(
                balanceDTO.AccountIdentifier
            );

            if (balance is null)
            {
                return NotFound();
            }

            balance.Amount = balanceDTO.Amount;

            await balanceRepository.UpdateAsync(balance);

            return NoContent();
        }
    }
}