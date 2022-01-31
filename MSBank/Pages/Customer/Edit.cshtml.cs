using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSBank.Models;
using System.ComponentModel.DataAnnotations;

namespace MSBank.Pages.Customer
{
    [BindProperties]
    public class EditModel : PageModel
    {
        private readonly BankAppDataContext _context;
        private readonly IMapper _mapper;

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

        public EditModel(BankAppDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public void OnGet(int customerId)
        {
            var customer=_context.Customers
                .First(c=>c.CustomerId== customerId);

            _mapper.Map(customer, this);

            //Gender = customer.Gender;
            //Givenname = customer.Givenname;
            //Surname = customer.Surname;
            //Streetaddress = customer.Streetaddress;
            //City = customer.City;
            //Country = customer.Country;
            //CountryCode = customer.CountryCode;
            //Birthday = customer.Birthday;
            //NationalId = customer.NationalId;
            //Telephonecountrycode = customer.Telephonecountrycode;
            //Telephonenumber = customer.Telephonenumber;
            //Emailaddress = customer.Emailaddress;
            //Zipcode = customer.Zipcode;
        }

        public IActionResult OnPost(int customerId)
        {
            if (ModelState.IsValid)
            {
                var customer = _context.Customers
               .First(c => c.CustomerId == customerId);

                _mapper.Map(this, customer);

                //customer.Gender=Gender;
                //customer.Givenname=Givenname;
                //customer.Surname=Surname;
                //customer.Streetaddress=Streetaddress;
                //customer.City=City;
                //customer.Country=Country;
                //customer.CountryCode=CountryCode;
                //customer.Birthday=Birthday;
                //customer.NationalId=NationalId;
                //customer.Telephonecountrycode=Telephonecountrycode;
                //customer.Telephonenumber=Telephonenumber;
                //customer.Emailaddress=Emailaddress;
                //customer.Zipcode=Zipcode;

                _context.SaveChanges();

                return RedirectToPage("Index");
            }

            return Page();
        }
    }
}
