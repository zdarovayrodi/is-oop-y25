namespace Banks.Accounts
{
    using Banks.Entities.Interfaces;

    public interface IAccount
    {
        IClient Client { get; }
        void Transfer(IAccount account, decimal amount);
    }
}