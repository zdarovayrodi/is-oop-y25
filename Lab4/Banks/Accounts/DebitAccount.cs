namespace Banks.Accounts
{
    using Banks.Entities.Interfaces;

    public abstract class DebitAccount : IAccount
    {
        public DebitAccount(IClient client, decimal interestRate)
        {
            Client = client;
            InterestRate = interestRate;
        }

        public IClient Client { get; }

        public decimal Balance { get; private set; }
        public decimal InterestRate { get; }
        public abstract void Transfer(IAccount account, decimal amount);
        public abstract void AddMoney(decimal amount);
        public abstract void WithdrawMoney(decimal amount);
    }
}