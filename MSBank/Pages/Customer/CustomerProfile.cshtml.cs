using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSBank.Models;
using MSBank.Services;
using MSBank.ViewModels;

namespace MSBank.Pages.Customer
{
    public class CustomerProfileModel : PageModel
    {
        private readonly BankAppDataContext _context;
     
        public decimal TotalBalance { get; set; }
        public List<CustomerViewModel> CustomerProfile { get; set; }

        public CustomerProfileModel(BankAppDataContext context)

        {
            _context = context;
            

        }
  
        public void OnGet(int customerId)
        {
            TotalBalance = _context.Dispositions.Where(r => r.CustomerId == customerId).Include(a => a.Account).Select(r => r.Account.Balance).Sum();

            CustomerProfile = _context.Dispositions

                .Include(a => a.Account)
                .Include(c => c.Customer)
                .Where(r => r.CustomerId == customerId)
             
                .Select(r => new CustomerViewModel
                {
                    CustomerId = r.Customer.CustomerId,
                    Gender = r.Customer.Gender,
                    Givenname = r.Customer.Givenname,
                    Surname = r.Customer.Surname,
                    Streetaddress = r.Customer.Streetaddress,
                    City = r.Customer.City,
                    Zipcode = r.Customer.Zipcode,
                    CountryCode = r.Customer.CountryCode,
                    Birthday = r.Customer.Birthday,
                    NationalId = r.Customer.NationalId,
                    Telephonecountrycode = r.Customer.Telephonecountrycode,
                    Emailaddress = r.Customer.Emailaddress,
                    AccountId = r.Account.AccountId,
                    Balance = r.Account.Balance,
                 
                }).ToList();
        }
    }
}

