using RamaBankProj.Enums;
using RamaBankProj.Model;
using RamaBankProj.Shared;

namespace RamaBankProj.Services
{
    public class SavingsAccountService : ISavingsAccountService
    {
        private readonly BankDbContext _dbContext;
        public SavingsAccountService(BankDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult> CalculateInterestAsync(int accountId)
        {
            var accnt = await _dbContext.Accounts.FindAsync(accountId);

            if (accnt == null)
            {
                return OperationResult.Failure($"Account with ID {accountId} not found.");
            }
            else if(accnt.Status == (int)Status.Frozen)
            {
                return OperationResult.Failure($"Account {accountId} is frozen and cannot be used for transactions.");
            }
            else if(accnt.AccountType != (int)AccountType.Savings)
            {
                return OperationResult.Failure($"Interest can only be calculated for Savings accounts. Account {accountId} is not a Savings account.");
            }

            var interestRate = 0.03m; // this value can configurable in appsettings.json
            var interest = accnt.Balance * interestRate;
            return OperationResult.Success(interest.ToString());
        }
    }
    
}