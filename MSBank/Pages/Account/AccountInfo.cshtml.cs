using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSBank.Models;
using MSBank.Services;
using MSBank.ViewModels;


namespace MSBank.Pages.Account
{
    public class AccountInfoModel : PageModel
    {
        private readonly BankAppDataContext _context;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public class AccountInfoViewModel
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; } = null!;
            public decimal Amount { get; set; }
            public decimal Balance { get; set; }
            public int AccountNumber { get; set; }

        }

        public int AccountId { get; set; }
        public decimal AccountBalance { get; set; }

        public List<CustomerViewModel> CustomerProfile { get; set; }
        public AccountInfoModel(BankAppDataContext context, IAccountService accountService, ITransactionService transactionService)

        {
            _context = context;
            _accountService = accountService;
            _transactionService = transactionService;
        }

        // public List<AccountInfoViewModel> CustomerTransactions { get; set; }

        //public IActionResult OnGetFetchValue(int id)
        //{
        //    return new JsonResult(new { value = id * 1000 });
        //}
        public void OnGet(int accountId)
        {
            var c = _context.Accounts.First(r => r.AccountId == accountId);

            AccountId = c.AccountId;
            AccountBalance= c.Balance;

            //added so info in the pictures shows
            CustomerProfile = _context.Dispositions

               .Include(a => a.Account)
               .Include(c => c.Customer)
               .Where(r => r.AccountId == accountId)
               .Select(r => new CustomerViewModel
               {
                   CustomerId = r.Customer.CustomerId,                
                   Givenname = r.Customer.Givenname,
                   Surname = r.Customer.Surname,
                   Emailaddress = r.Customer.Emailaddress,
                   AccountId = r.Account.AccountId,
                   Balance = r.Account.Balance,


               }).ToList();

        }

        public IActionResult OnGetFetchMore(int accountId, long lastTicks)
        {
           


            var list = _transactionService.GetAllTransactions(accountId, lastTicks)


                .Select(r => new AccountInfoViewModel
                {
                    Id = r.TransactionId,
                    Date = r.Date,
                    Type = r.Type,
                    Amount = r.Amount,
                    Balance = r.Balance,
                    AccountNumber=r.AccountId

                }).ToList();

            if (list.Any())
                lastTicks = list.Last().Date.Ticks;
            return new JsonResult(new { items = list, lastTicks });
        }
    }
   
}
