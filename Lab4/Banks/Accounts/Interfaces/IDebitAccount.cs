namespace Banks.Accounts.Interfaces
{
    using Banks.Entities.Interfaces;
    public interface IDebitAccount
    {
        IClient Client { get; }
        decimal Balance { get; }
        double InterestRate { get; }
        void Withdraw(decimal amount);
        void ApplyInterest();
        void CalculateDailyInterest();
    }
}