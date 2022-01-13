using MSBank.Models;

namespace MSBank.Services
{
    public class AccountService :IAccountService
    {

        private readonly BankAppDataContext _context;
        public AccountService(BankAppDataContext context)

        {
            _context = context;
        }

        public List<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public void Update(Account account)
        {
            _context.SaveChanges();
        }

        public Account GetAccount(int id)
        {
            return _context.Accounts.First(e => e.AccountId == id);
        }

    }
}
