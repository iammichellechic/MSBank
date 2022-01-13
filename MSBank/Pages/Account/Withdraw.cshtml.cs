using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSBank.Services;
using System.ComponentModel.DataAnnotations;

namespace MSBank.Pages.Account
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        [Range(10, 3000)]
        public int Amount { get; set; }


        public WithdrawModel(IAccountService accountService, ITransactionService transactionService)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        public void OnGet(int accountId)
        {
            Amount = 100;
        }

        public IActionResult OnPost(int accountId)
        {

            if (ModelState.IsValid)
            {
                var status = _transactionService.Withdraw(accountId, Amount);
                if (status == ITransactionService.ErrorCode.Ok)
                {
                    return RedirectToPage("Index");
                }
                ModelState.AddModelError("Amount", "Beloppet är fel");
            }

            return Page();


            //var account = _accountService.GetAccount(accountId);
            //if (account.Balance < Amount)
            //{
            //    ModelState.AddModelError("Amount", "För stort belopp");
            //}



            //    if (ModelState.IsValid)
            //{
            //    account.Balance -= Amount;
            //    _accountService.Update(account);
            //    return RedirectToPage("Index");
            //}

            return Page();
        }

    }
}
