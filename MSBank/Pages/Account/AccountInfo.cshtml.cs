using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MSBank.Models;

namespace MSBank.Pages.Account
{
    public class AccountInfoModel : PageModel
    {
        private readonly BankAppDataContext _context;

        public class AccountInfoViewModel
        {
            public int Id { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; } = null!;
            public decimal Amount { get; set; }
            public decimal Balance { get; set; }

        }

        public int AccountId { get; set; }
        public decimal TotalBalance { get; set; }

        public AccountInfoModel(BankAppDataContext context)

        {
            _context = context;

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
            TotalBalance = c.Balance;


            //CustomerTransactions = _context.Transactions

            //    .Where(r => r.AccountId == accountId)
            //    .OrderByDescending(r => r.Date)
            //    .Select(r => new AccountInfoViewModel
            //    {
            //        TransactionId = r.TransactionId,
            //        Date = r.Date,
            //        Type = r.Type,
            //        Amount = r.Amount,
            //        Balance = r.Balance,

            //    }).ToList();

        }

        public IActionResult OnGetFetchMore(int accountId, long lastTicks)
        {
            DateTime dateOfLastShown = new DateTime(lastTicks).AddMilliseconds(100);

            var list = _context.Transactions

                .Where(r => r.AccountId == accountId)
                .Where(d => lastTicks == 0 || d.Date > dateOfLastShown)
                .OrderByDescending(r => r.Date)
                .Take(20)
                .Select(r => new AccountInfoViewModel
                {
                    Id = r.TransactionId,
                    Date = r.Date,
                    Type = r.Type,
                    Amount = r.Amount,
                    Balance = r.Balance,

                }).ToList();

            if (list.Any())
                lastTicks = list.Last().Date.Ticks;
            return new JsonResult(new { items = list, lastTicks });
        }
    }
   
}
