namespace Banks.Accounts
{
    using Banks.Accounts.Interfaces;
    using Banks.Entities.Interfaces;
    using Banks.Models;

    public class DepositAccount : IAccount
    {
        private List<Transaction> _transactions = new List<Transaction>();

        public DepositAccount(IClient client, IReadOnlyList<DepositInterestRates> interestRates, decimal balance, DateOnly endDate)
        {
            if (interestRates == null) throw new ArgumentNullException(nameof(interestRates));
            if (interestRates.Count == 0) throw new ArgumentException("Interest rates cannot be empty", nameof(interestRates));
            if (balance < 0) throw new ArgumentException("Balance cannot be negative", nameof(balance));
            if (endDate < DateOnly.FromDateTime(DateTime.Today)) throw new ArgumentException("End date cannot be in the past", nameof(endDate));

            Client = client;
            InterestRates = interestRates;
            Balance = balance;
            EndDate = endDate;
        }

        public IClient Client { get; }
        public decimal Balance { get; private set; }
        public IReadOnlyList<Transaction> Transactions => _transactions.AsReadOnly();

        public Transaction Transfer(IAccount account, decimal amount)
        {
            if (account == null) throw new ArgumentNullException(nameof(account));
            if (amount <= 0) throw new ArgumentException("Amount must be positive", nameof(amount));
            if (Balance < amount) throw new ArgumentException("Insufficient funds", nameof(amount));
            if (account == this)
                throw new InvalidOperationException("Can't transfer money to the same account");

            Balance -= amount;
            account.Deposit(amount);
            var transaction = new Transaction(this, account, amount);
            _transactions.Add(transaction);
            return transaction;
        }

        public Transaction Withdraw(decimal amount)
        {
            if (EndDate > DateOnly.FromDateTime(DateTime.Today)) throw new InvalidOperationException("Cannot transfer money from a deposit account before the end date");
            if (amount <= 0) throw new ArgumentException("Amount must be positive", nameof(amount));
            if (Balance < amount) throw new ArgumentException("Insufficient funds", nameof(amount));

            var transaction = new Transaction(this, null, amount);
            _transactions.Add(transaction);
            Balance -= amount;
            return transaction;
        }

        public Transaction Deposit(decimal amount)
        {
            throw new Exception("Cannot deposit money into a deposit account");
        }

        public void RevertTransaction(Transaction transaction)
        {
            if (transaction == null) throw new ArgumentNullException(nameof(transaction));
            if (!_transactions.Contains(transaction)) throw new ArgumentException("Transaction does not belong to this account", nameof(transaction));

            transaction.Revert();
            _transactions.Remove(transaction);
        }

        public IReadOnlyList<DepositInterestRates> InterestRates { get; }
        public DateOnly EndDate { get; }
        public void ApplyMonthlyInterest()
        {
            Balance += Balance * (decimal)CurrentInterestRate();
        }

        private decimal CurrentInterestRate()
        {
            decimal currentInterestRate = 0;
            foreach (DepositInterestRates interestRate in InterestRates)
            {
                if (Balance >= interestRate.Money) currentInterestRate = interestRate.InterestRate;
            }

            return currentInterestRate / 12;
        }
    }
}