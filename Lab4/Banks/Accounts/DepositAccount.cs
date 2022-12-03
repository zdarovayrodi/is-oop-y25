using Banks.Models;

namespace Banks.Accounts
{
    using Banks.Accounts.Interfaces;
    using Banks.Entities.Interfaces;

    public class DepositAccount : IAccount
    {
        public DepositAccount(IClient client, Dictionary<decimal, double> interestRates, decimal balance, DateOnly endDate)
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
        public Trasaction Transfer(IAccount account, decimal amount)
        {
            throw new NotImplementedException();
        }

        public Trasaction Withdraw(decimal amount)
        {
            throw new NotImplementedException();
        }

        public Trasaction Deposit(decimal amount)
        {
            throw new NotImplementedException();
        }

        public IReadOnlyDictionary<decimal, double> InterestRates { get; }
        public DateOnly EndDate { get; }
        public void ApplyMonthlyInterest()
        {
            Balance += Balance * (decimal)CurrentInterestRate();
        }

        private double CurrentInterestRate()
        {
            double currentInterestRate = 0;
            foreach (var interestRate in InterestRates)
            {
                if (Balance >= interestRate.Key) currentInterestRate = interestRate.Value;
            }

            return currentInterestRate / 12;
        }
    }
}