﻿namespace MSBank.Services
{
    public interface ITransactionService
    {
        bool Deposit(int accountId, int belopp);

        bool CanWithdraw(int accountId, int belopp);

        public enum ErrorCode
        {
            Ok,
            BalanceTooLow,
        }
        ErrorCode Withdraw(int accountId, int belopp);
        // validate - är belopp ok, är belopp < balance
        // get account uipdate balance, create transaction
    }
}
