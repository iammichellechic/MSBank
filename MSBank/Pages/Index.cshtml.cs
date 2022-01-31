using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSBank.Data;
using MSBank.Models;
using MSBank.Services;
using MSBank.ViewModels;

namespace MSBank.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _dbcontext;
        private readonly BankAppDataContext _context;

        public int TotalCustomers { get; set; }
        public int TotalAccounts { get; set; }
        public decimal TotalAccountBalance { get; set; }
        public IndexModel(ILogger<IndexModel> logger, ApplicationDbContext dbcontext, BankAppDataContext context)
        {
            _logger = logger;
            _dbcontext = dbcontext;
            _context = context;
        }

      
        public void OnGet()
        {
          

            TotalCustomers = _context.Customers.Count();
            TotalAccounts = _context.Accounts.Count();
            TotalAccountBalance = _context.Accounts.Select(a=>a.Balance).Sum();
        }

      
    }
}