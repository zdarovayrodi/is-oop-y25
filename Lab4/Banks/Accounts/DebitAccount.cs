namespace Banks.Accounts
{
    using Banks.Accounts.Interfaces;
    using Banks.Entities.Interfaces;

    public class DebitAccount : IDebitAccount
    {
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
        public double InterestRate { get; } = 0;

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