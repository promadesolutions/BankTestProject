using RamaBankProj.Enums;
using RamaBankProj.Model;

namespace RamaBankProj.Services
{
    public interface IBankAccountService
    {
        Task<bool> AccountExistsAsync(string firstName, string lastName, AccountType accountType);
        Task CreateAccountAsync(Account account);
        Task<Account> GetAccountByIdAsync(int accountId);
        Task<Account> GetAccountByAccountNumberAsync(string accountNumber);
        Task FreezeAccountAsync(Account account);
    }
}