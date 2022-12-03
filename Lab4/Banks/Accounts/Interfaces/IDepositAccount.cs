using Banks.Entities.Interfaces;

namespace Banks.Accounts.Interfaces;

public interface IDepositAccount
{
    IClient Client { get; }
    decimal Balance { get; }
    IReadOnlyDictionary<decimal, double> InterestRates { get; }
    DateOnly EndDate { get; }
    void ApplyMonthlyInterest();
}