namespace Banks.Accounts
{
    using Banks.Entities.Interfaces;

    public abstract class DepositAccount : IAccount
    {
        protected DepositAccount(IClient client, DateOnly closeDate, decimal interestRate)
        {
            Client = client;
            CloseDate = closeDate;
        }

        public IClient Client { get; }
        public decimal Balance { get; private set; }
        public DateOnly CloseDate { get; }

        public abstract void Transfer(IAccount account, decimal amount);
        public abstract void AddMoney(decimal amount);
        public abstract void WithdrawMoney(decimal amount);
    }
}