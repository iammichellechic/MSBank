using MSBank.Models;

namespace MSBank.Services
{
    public interface IAccountService
    {
        public List<Account> GetAll(string q);

        public List<Transaction> GetAllTransactions(int accountId, long lastTicks);

        void Update(Account account);

        //void CreateTransaction(int accountId, int amount);
        Account GetAccount(int id);
    }
}
