﻿using MSBank.Models;

namespace MSBank.Services
{
    public interface IAccountService
    {
        public List<Account> GetAll();

        void Update(Account account);
        Account GetAccount(int id);
    }
}
