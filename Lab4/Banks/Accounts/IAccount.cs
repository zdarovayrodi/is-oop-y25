namespace Banks.Accounts
{
    using Banks.Entities.Interface;

    public interface IAccount
    {
        IClient Client { get; }
        void Transfer(IAccount account, decimal amount);
    }
}