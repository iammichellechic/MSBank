using Microsoft.AspNetCore.Mvc;
using MSBank.Models;

namespace MSBank.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _context;
        public TransactionService(BankAppDataContext context)

        {
            _context = context;
        }

        public void CreateTransaction(Transaction transaction)
        {
            _context.SaveChanges();
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

        //public void OnPostCreateTransaction()
        //{
            
        //        var customer = new Models.Transaction
        //        {
                    
        //            //  Disposition=_context.Dispositions.First(e => e.DispositionId == DispositionId)

               
        //        _context.Transactions.Add();
        //        _context.SaveChanges();

        //    }
        }
}
