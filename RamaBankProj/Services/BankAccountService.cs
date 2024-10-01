using Microsoft.EntityFrameworkCore;
using RamaBankProj.Enums;
using RamaBankProj.Model;

namespace RamaBankProj.Services
{
    public class BankAccountService : IBankAccountService
    {
        private readonly BankDbContext _dbContext;
        public BankAccountService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> AccountExistsAsync(string firstName, string lastName, AccountType accountType)
        {
            return await _dbContext.Accounts.AnyAsync(a => a.FirstName == firstName &&
                                                a.LastName == lastName &&
                                                a.AccountType == (int)accountType);
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _dbContext.Accounts.AddAsync(account);
            await _dbContext.SaveChangesAsync();
        }

        public async Task FreezeAccountAsync(Account account)
        {
            account.Status = (int)Status.Frozen;
            _dbContext.Accounts.Update(account);
            
            await _dbContext.SaveChangesAsync();
        }

        public Task<Account> GetAccountByAccountNumberAsync(string accountNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> GetAccountByIdAsync(int accountId)
        {
            var customer = await _dbContext.Accounts.FindAsync(accountId);
            if (customer == null)
            {
                throw new InvalidOperationException($"Account with ID {accountId} not found.");
            }
            return customer;
        }
    }
}