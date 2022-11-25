namespace Banks.Accounts
{
    using Banks.Entities.Interface;

    public abstract class CreditAccount : IAccount
    {
        protected CreditAccount(IClient client, decimal comission)
        {
            Client = client;
            Comission = comission;
        }

        public IClient Client { get; }
        public decimal Comission { get; }
        public decimal CreditLimit { get; private set; }

        public abstract void Transfer(IAccount account, decimal amount);
        public void SetCreditLimit(decimal amount)
        {
            CreditLimit = amount;
        }
    }
}