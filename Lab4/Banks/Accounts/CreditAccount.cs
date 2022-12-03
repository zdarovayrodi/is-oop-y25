using Banks.Models;

namespace Banks.Accounts
{
    using Banks.Accounts.Interfaces;
    using Banks.Entities.Interfaces;

    public class CreditAccount : IAccount
    {
        public CreditAccount(IClient client, decimal creditLimit, decimal fixedCommission)
        {
            if (creditLimit < 0)
                throw new ArgumentOutOfRangeException("Credit limit can't be negative");
            if (fixedCommission < 0)
                throw new ArgumentOutOfRangeException("Fixed commission can't be negative");
            Client = client ?? throw new ArgumentNullException("Client can't be null");
            CreditLimit = creditLimit;
            FixedCommission = fixedCommission;
        }

        public IClient Client { get; }
        public decimal Balance { get; private set; } = 0;
        public Trasaction Transfer(IAccount account, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Trasaction Withdraw(decimal amount)
        {
            throw new NotImplementedException();
        }

        public Trasaction Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }

        public decimal CreditLimit { get; }
        public decimal FixedCommission { get; }
        public void ApplyDailyCommission()
        {
            if (Balance < 0 && Balance > -CreditLimit)
                Balance -= FixedCommission;
        }
    }
}