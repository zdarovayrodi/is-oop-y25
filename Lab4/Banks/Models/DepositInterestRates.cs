namespace Banks.Models
{
    public class DepositInterestRates
    {
        public DepositInterestRates(decimal money, decimal interestRate)
        {
            Money = money;
            InterestRate = interestRate;
        }

        public decimal Money { get; }
        public decimal InterestRate { get; }
    }
}