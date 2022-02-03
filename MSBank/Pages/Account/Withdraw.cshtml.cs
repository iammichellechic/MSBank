using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSBank.Services;
using System.ComponentModel.DataAnnotations;

namespace MSBank.Pages.Account
{
    [BindProperties]

    [Authorize(Roles = "Admin, Cashier")]
    public class WithdrawModel : PageModel
    {
        private readonly IAccountService _accountService;
    

        [Range(10, 3000)]
        [Required(ErrorMessage = "Please provide amount")]
        public int Amount { get; set; }


        public WithdrawModel(IAccountService accountService)
        {
            _accountService = accountService;
       
        }

        public void OnGet(int accountId)
        {
            Amount = 100;
        }

        public IActionResult OnPost(int accountId)
        {

            //if (ModelState.IsValid)
            //{
            //    var status = _transactionService.Withdraw(accountId, Amount);
            //    if (status == ITransactionService.ErrorCode.Ok)
            //    {
            //        return RedirectToPage("Index");
            //    }
            //    ModelState.AddModelError("Amount", "Beloppet är fel");
            //}

            //return Page();


            var account = _accountService.GetAccount(accountId);
            if (account.Balance < Amount)
            {
                ModelState.AddModelError("Amount", "För stort belopp");
            }

            if (ModelState.IsValid)
            {
                account.Balance -= Amount;
                _accountService.Update(account);
              
                return RedirectToPage("Index");
            }

            return Page();
        }

    }
}
