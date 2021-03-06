using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MSBank.Models;
using System.ComponentModel.DataAnnotations;

namespace MSBank.Pages.Customer
{
    [BindProperties]
    public class NewModel : PageModel
    {
        private readonly BankAppDataContext _context;
      
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

        public NewModel(BankAppDataContext context)
        {
            _context = context;
         
        }

        public IActionResult OnPost()
        {
           if (ModelState.IsValid)
            {
                var customer = new Models.Customer
                {
                    Gender = Gender,
                    Givenname = Givenname,
                    Surname = Surname,
                    Streetaddress = Streetaddress,
                    City = City,
                    Country = Country,
                    CountryCode = CountryCode,
                    Birthday = Birthday,
                    NationalId = NationalId,
                    Telephonecountrycode = Telephonecountrycode,
                    Telephonenumber = Telephonenumber,
                    Emailaddress = Emailaddress,
                    Zipcode = Zipcode,
              

                };

                _context.Customers.Add(customer);
                _context.SaveChanges();

                return RedirectToPage("Index");
            }

          return Page();
         }
        
    }
}
