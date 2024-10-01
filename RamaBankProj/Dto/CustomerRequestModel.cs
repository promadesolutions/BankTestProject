using System.ComponentModel.DataAnnotations;

namespace RamaBankProj.Dto
{
    public class CustomerRequestModel
    {
        [Required]
        public string FirstName { get; set; }   
        public string LastName { get; set; }
        
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        public string Phone { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string Postcode { get; set; }

        [Required]
        public int AccountType { get; set; }

        public decimal Balance { get; set; }
    }
}