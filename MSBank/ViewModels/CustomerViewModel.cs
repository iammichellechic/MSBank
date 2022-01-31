using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MSBank.ViewModels
{
    public class CustomerViewModel
    {

        public int CustomerId { get; set; }

        public string Gender { get; set; } = null!;

        [MaxLength(100)]
        [Required]
        public string Givenname { get; set; } = null!;

        [MaxLength(100)]
        [Required]
        public string Surname { get; set; } = null!;

        [StringLength(100)]   
        public string Streetaddress { get; set; } = null!;

        [StringLength(50)]    
        public string City { get; set; } = null!;
        [StringLength(50)]
        public string Zipcode { get; set; } = null!;
        [StringLength(50)]
        public string Country { get; set; } = null!;
        [StringLength(50)]
        public string CountryCode { get; set; } = null!;

         public DateTime? Birthday { get; set; }
         public string? NationalId { get; set; }
         public string? Telephonecountrycode { get; set; }
         public string? Telephonenumber { get; set; }

        [StringLength(150)]
        [EmailAddress]
        public string? Emailaddress { get; set; }

        public int AccountId { get; set; }
        public decimal Balance { get; set; }
        
    }

}
