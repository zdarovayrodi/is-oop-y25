namespace Banks.Accounts.Interfaces
{
    using Banks.Entities;
    using Banks.Models;

    public interface IAccount
    {
        Client Client { get; }
        decimal Balance { get; }
        IReadOnlyList<Transaction> Transactions { get; }
        Transaction Transfer(IAccount account, decimal amount);
        Transaction Withdraw(decimal amount);
        Transaction Deposit(decimal amount);
        void RevertTransaction(Transaction transaction);
    }
}