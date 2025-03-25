using ApiTest.Views;

namespace ApiTest.Services.Domain
{
    public interface IAccountService
    {
        Task<AccountView> CreateAccountAsync(string cnpj);
    }
}