using RamaBankProj.Model;
using RamaBankProj.Shared;

namespace RamaBankProj.Services
{
    public interface ISavingsAccountService
    {
        Task<OperationResult> CalculateInterestAsync(int accountId);
    }
}