namespace RamaBankProj.Model;

public class Account
{
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string PhonePrimary { get; set; } = string.Empty;
    public string PhoneSecondary { get; set; } = string.Empty;
    public int Status { get; set; }
    public decimal Balance { get; set; } = 0.0m;
    public DateTime CreatedOn { get; set; }
    public DateTime UpdatedOn { get; set; }
    public int AccountType { get; set; }
}