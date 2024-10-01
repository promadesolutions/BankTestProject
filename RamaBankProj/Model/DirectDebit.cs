namespace RamaBankProj.Model
{
    public class DirectDebit
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DebitDate { get; set; }
        public int Status { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}