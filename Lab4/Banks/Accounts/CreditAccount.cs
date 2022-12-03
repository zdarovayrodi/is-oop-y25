namespace Banks.Accounts
{
    using Banks.Accounts.Interfaces;
    using Banks.Entities.Interfaces;
    using Banks.Models;

    public class CreditAccount : IAccount
    {
        private List<Transaction> _transactions = new List<Transaction>();
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
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
        public Transaction Transfer(IAccount account, decimal amount)
        {
            if (account == null)
                throw new ArgumentNullException("Account can't be null");
            if (amount < 0)
                throw new ArgumentOutOfRangeException("Amount can't be negative");
            if (Balance - amount < -CreditLimit)
                throw new InvalidOperationException("Not enough money on account");
            if (account == this)
                throw new InvalidOperationException("Can't transfer money to the same account");

            var transaction = new Transaction(this, account, amount);
            _transactions.Add(transaction);
            Balance -= amount;
            account.Deposit(amount);
            return transaction;
        }

        public Transaction Withdraw(decimal amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("Amount can't be negative");
            if (Balance - amount < -CreditLimit) throw new InvalidOperationException("Not enough money");

            var transaction = new Transaction(this, null, amount);
            _transactions.Add(transaction);
            Balance -= amount;
            return transaction;
        }

        public Transaction Deposit(decimal amount)
        {
            if (amount < 0) throw new ArgumentOutOfRangeException("Amount can't be negative");

            var transaction = new Transaction(null, this, amount);
            _transactions.Add(transaction);
            Balance += amount;
            return transaction;
        }

        public void RevertTransaction(Transaction transaction)
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