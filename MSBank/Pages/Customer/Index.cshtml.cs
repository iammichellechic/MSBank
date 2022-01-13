using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSBank.Models;
using MSBank.Services;


namespace MSBank.Pages.Customer
{
    [Authorize(Roles="Admin")]
    public class IndexModel : PageModel
    {
        private readonly BankAppDataContext _context;
        private readonly ICustomerService _customerService;
        public IndexModel(BankAppDataContext context, ICustomerService customerService)

        {
            _context = context;
            _customerService = customerService;
        }

        public class CustomerViewModel
        {
            //En lista ska visas med kundnummer och personnummer, namn, adress, city
            public int CustomerId { get; set; }
            public string Givenname { get; set; }
            public string Surname { get; set; }
            public string Streetaddress { get; set; }
            public string City { get; set; }
            public string NationalId { get; set; }
        }
        public string Q { get; set; }
        public string SortOrder { get; set; }
        public string SortColumn { get; set; }
     
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public List<CustomerViewModel> Customers { get; set; } 
        public void OnGet(string q, string sortColumn, string sortOrder,
            int pageno)
        {
            Q = q;
            SortOrder = sortOrder;
            SortColumn = sortColumn;
            if (pageno == 0)
                pageno = 1;
            CurrentPage = pageno;

            var pageresult = _customerService.GetAll(sortColumn, sortOrder, CurrentPage, q);

            PageCount = pageresult.PageCount;

            Customers = pageresult.Results
                
                .Select(c => new CustomerViewModel
                {
                    CustomerId = c.CustomerId,
                    Givenname = c.Givenname,
                    Surname = c.Surname,
                    Streetaddress = c.Streetaddress,
                    City = c.City,
                    NationalId = c.NationalId
                }).ToList();

        }
    }
}
