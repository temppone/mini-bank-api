using ApiTest.DTOs;
using ApiTest.Repositories.Interfaces;
using ApiTest.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController(
        IAccountRepository accountRepository,
        AccountService accountService
        ) : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var accounts = await accountRepository.GetAllAsync();

            return Ok(accounts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var account = await accountRepository.GetByIdAsync(id);
            if (account == null)
                return NotFound();
            return Ok(account);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] string Cnpj)
        {
            if (Cnpj == null)
                return BadRequest("Account data is required.");

            var account = await accountService.CreateAccountAsync(Cnpj);

            if (account == null)
                return BadRequest("Failed to create account.");

            return CreatedAtAction(nameof(GetById), new { id = account.Identifier }, account);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] AccountDTO accountDto)
        {
            if (accountDto == null)
                return BadRequest("Account data is required.");

            var account = await accountRepository.GetByIdAsync(id);
            if (account == null)
                return NotFound();

            account.Name = accountDto.Name;

            var updated = await accountRepository.UpdateAsync(account);

            if (!updated)
                return BadRequest("Failed to update account.");

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var deleted = await accountRepository.DeleteAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}