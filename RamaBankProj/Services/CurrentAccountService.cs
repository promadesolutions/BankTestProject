using RamaBankProj.Enums;
using RamaBankProj.Model;
using RamaBankProj.Shared;

namespace RamaBankProj.Services
{
    public class CurrentAccountService : ICurrentAccountService
    {
        private readonly BankDbContext _dbContext;

        public CurrentAccountService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> ScheduleDirectDebitAsync(int accountId, decimal amount, DateTime scheduledDate)
        {
            var account = await _dbContext.Accounts.FindAsync(accountId);

            if(account == null)
            {
                return OperationResult.Failure($"Account with ID {accountId} not found.");
            }
            else if(account.AccountType != (int)AccountType.Current)
            {
                return OperationResult.Failure($"Direct debits can only be scheduled for Current accounts. Account {accountId} is not a Current account.");
            }
            else if (account.Status == (int)Status.Frozen)
            {
                return OperationResult.Failure($"Account {accountId} is frozen and cannot be used for transactions.");
            }
            
            _dbContext.DirectDebits.Add(new DirectDebit
                {
                    AccountId = accountId,
                    Amount = amount,
                    DebitDate = scheduledDate,
                    Status = 1
                });
                await _dbContext.SaveChangesAsync();
                return OperationResult.Success("Direct debit scheduled successfully");
        }
    }
}