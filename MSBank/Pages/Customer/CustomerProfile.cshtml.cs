using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSBank.Models;
using MSBank.Services;

namespace MSBank.Pages.Customer
{
    public class CustomerProfileModel : PageModel
    {
        private readonly BankAppDataContext _context;
       
        public class CustomerProfileViewModel
        {
            public int CustomerId { get; set; }
            public string Gender { get; set; } = null!;
            public string Givenname { get; set; } = null!;
            public string Surname { get; set; } = null!;
            public string Streetaddress { get; set; } = null!;
            public string City { get; set; } = null!;
            public string Zipcode { get; set; } = null!;
            public string Country { get; set; } = null!;
            public string CountryCode { get; set; } = null!;
            public DateTime? Birthday { get; set; }
            public string? NationalId { get; set; }
            public string? Telephonecountrycode { get; set; }
            public string? Telephonenumber { get; set; }
            public string? Emailaddress { get; set; }

            public decimal TotalBalance { get; set; }
            public int AccountId { get; set; }
            public decimal Balance { get; set; }
           
        }

        public string Q { get; set; }

        //needs like a service for this to work like GetAccount(int accountId)
        public int CustomersAccountId { get; set; }



        public List<CustomerProfileViewModel> CustomerProfile { get; set; }

        //public class CustomerAccountsInfoViewModel 
        //{
        //    public int AccountId { get; set; }
        //    public decimal Balance { get; set; }
        //}


        public CustomerProfileModel(BankAppDataContext context)

        {
            _context = context;
      
        }
  
        public void OnGet(int customerId, string q)
        {

            CustomerProfile = _context.Dispositions

                .Include(a => a.Account)
                .Include(c => c.Customer)
                .Where(r => r.CustomerId == customerId)
                .Where(r => q == null || r.CustomerId.ToString().Contains(q))
                .Select(r => new CustomerProfileViewModel
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
                    //TotalBalance = a.Account.Balance.Sum();
                    AccountId = r.Account.AccountId,
                    Balance = r.Account.Balance,
                 

                }).ToList();
            
              
               

          //      Q = _context.Customers.Where(r => r.CustomerId.ToString().Contains(q)).First();
              

            //var query = _context.Customers.AsQueryable();

            //if (!string.IsNullOrEmpty(q))
            //{
            //    query = query
            //            .Where(r => q == null || r.CustomerId.ToString().Contains(q));
            //}

           


            //CustomerAccounts = _context.Accounts

            //    . Select(i=> new CustomerAccountsInfoViewModel
            //{
            //    AccountId = i.AccountId,
            //    Balance = i.Balance,

            //}).ToList();

        }
    }
}

