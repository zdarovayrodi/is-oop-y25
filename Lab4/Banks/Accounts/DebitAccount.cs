namespace Banks.Accounts
{
    using Banks.Accounts.Interfaces;
    using Banks.Entities.Interfaces;
    using Banks.Models;

    public class DebitAccount : IAccount
    {
        private List<Transaction> _transactions = new List<Transaction>();

        private double _appliedInterestBalance = 0;
        public DebitAccount(IClient client, double interestRate = 0)
        {
            if (interestRate < 0)
                throw new ArgumentException("Interest rate cannot be negative");
            Client = client ?? throw new ArgumentNullException("Client can't be null");
            Client = client;
            InterestRate = interestRate;
        }

        public IClient Client { get; }
        public decimal Balance { get; private set; } = 0;
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();
        public double InterestRate { get; } = 0;

        public Transaction Transfer(IAccount account, decimal amount)
        {
            if (account == null) throw new ArgumentNullException("Account can't be null");
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            if (Balance < amount) throw new ArgumentException("Not enough money on account");

            var transaction = new Transaction(this, account, amount);
            Balance -= amount;
            account.Deposit(amount);
            _transactions.Add(transaction);
            return transaction;
        }

        Transaction IAccount.Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");
            if (Balance < amount) throw new ArgumentException("Not enough money on account");

            var transaction = new Transaction(this, null, amount);
            Balance -= amount;
            _transactions.Add(transaction);
            return transaction;
        }

        public Transaction Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Amount must be positive");

            var transaction = new Transaction(null, this, amount);
            Balance += amount;
            _transactions.Add(transaction);
            return transaction;
        }

        public void RevertTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException("Transaction can't be null");
            if (!_transactions.Contains(transaction)) throw new ArgumentException("Transaction not found");

            transaction.Revert();
            _transactions.Remove(transaction);
        }

        public void Withdraw(decimal amount)
        {
            if (amount > Balance) throw new InvalidOperationException("Insufficient funds");
            if (amount <= 0) throw new InvalidOperationException("Amount must be positive");
            Balance -= amount;
        }

        public void CalculateDailyInterest()
        {
            _appliedInterestBalance += (double)Balance * DailyInterestRate;
        }

        public void ApplyInterest()
        {
            Balance += (decimal)_appliedInterestBalance;
            _appliedInterestBalance = 0;
        }

        private double DailyInterestRate => InterestRate / 365;
    }
}