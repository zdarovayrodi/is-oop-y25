namespace Banks.Accounts
{
    using Banks.Accounts.Interfaces;
    using Banks.Entities.Interfaces;

    public class DepositAccount : IDepositAccount
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
        public decimal Balance { get; }
        public IReadOnlyDictionary<decimal, double> InterestRates { get; }
        public DateOnly EndDate { get; }
    }
}