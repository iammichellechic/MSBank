using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSBank.Services;

namespace MSBank.Pages.Account
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _accountService;

        public List<AccountViewModel> CustomerAccount { get; set; }

        public class AccountViewModel
        {
            public int AccountNumber { get; set; }
            public decimal Balance { get; set; }
        }

        public string Q { get; set; }

        public IndexModel(IAccountService accountService)
        {
            _accountService = accountService;
        }

        public void OnGet(string q)
        {
            Q = q;
            
            CustomerAccount = _accountService.GetAll(Q).Select(r => new AccountViewModel
            {      
                AccountNumber = r.AccountId,
                Balance = r.Balance
            }).ToList();
        }
    }
}
