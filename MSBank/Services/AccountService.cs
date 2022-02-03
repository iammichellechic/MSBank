using Microsoft.EntityFrameworkCore;
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

        public List<Account> GetAll(string q)
        {
            var query = _context.Accounts.AsQueryable();

            if (!string.IsNullOrEmpty(q))
            {
                query = query
                        .Where(r => q == null || r.AccountId.ToString().Contains(q));

            }

            return query.ToList();
        }

        public List<Transaction> GetAllTransactions(int accountId, long lastTicks)

        {
            DateTime dateOfLastShown = new DateTime(lastTicks).AddMilliseconds(100);

            return _context.Transactions
            
                .Where(r => r.AccountId == accountId)
                .Where(d => lastTicks == 0 || d.Date > dateOfLastShown)
                .OrderByDescending(r => r.Date)
                .Take(20)
                .ToList();
        }
        public void Update(Account account)
        {
            
            _context.SaveChanges();
        }

        public Account GetAccount(int accountId)
        {
            return _context.Accounts.First(e => e.AccountId == accountId);
        }

     

    }
}
