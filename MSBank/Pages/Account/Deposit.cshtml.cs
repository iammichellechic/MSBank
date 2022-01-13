using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MSBank.Services;
using System.ComponentModel.DataAnnotations;

namespace MSBank.Pages.Account
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly IAccountService _accountService;

        [Range(10, 3000)]
        public int Amount { get; set; }
        public DateTime Created { get; set; }

        [Required(ErrorMessage = "Write a comment")]
        [MinLength(5)]
        [MaxLength(100)]
        public string Comment { get; set; }

        public DepositModel(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public void OnGet(int accountId)
        {
            Created = DateTime.Now.AddDays(1).Date;
            Amount = 100;
        }
        public IActionResult OnPost(int accountId)
        {
            if (Created < DateTime.Now.AddDays(1).Date)
            {
                ModelState.AddModelError("Created", "Datum måste vara minst en dag fram");
            }
            if (ModelState.IsValid)
            {
                //bad practice
                var account = _accountService.GetAccount(accountId);
                account.Balance += Amount;
                _accountService.Update(account);
                return RedirectToPage("Index");

            }
            return Page();

            //hämta account from db
            //balance +=Amount
            //hoppa tillbaka till kontolistan
        }


    }
}
