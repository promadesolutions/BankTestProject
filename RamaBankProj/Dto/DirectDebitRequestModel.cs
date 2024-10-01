namespace RamaBankProj.Dto
{
    public class DirectDebitRequestModel
    {
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime ScheduledDate { get; set; }
    }
}