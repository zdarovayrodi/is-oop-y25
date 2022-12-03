namespace Banks.Accounts.Interfaces
{
    using Banks.Entities.Interfaces;
    using Banks.Models;

    public interface IAccount
    {
        IClient Client { get; }
        decimal Balance { get; }
        Trasaction Transfer(IAccount account, decimal amount);
        Trasaction Withdraw(decimal amount);
        Trasaction Deposit(decimal amount);
    }
}