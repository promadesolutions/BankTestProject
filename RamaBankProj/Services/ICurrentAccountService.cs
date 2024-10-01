using RamaBankProj.Shared;

namespace RamaBankProj.Services
{
    public interface ICurrentAccountService
    {
        Task<OperationResult> ScheduleDirectDebitAsync(int accountId, decimal amount, DateTime scheduledDate);
    }
}