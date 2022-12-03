namespace Banks.Models
{
    using Banks.Accounts.Interfaces;

    public class Transaction
    {
        public Transaction(IAccount? from, IAccount? to, decimal amount)
        {
            From = from;
            To = to;
            Amount = amount;
        }

        public IAccount? From { get; }
        public IAccount? To { get; }
        public decimal Amount { get; }

        public void Revert()
        {
            From?.Withdraw(Amount);
            To?.Deposit(Amount);
        }
    }
}