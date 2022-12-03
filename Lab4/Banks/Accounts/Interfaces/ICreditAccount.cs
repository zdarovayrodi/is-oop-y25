namespace Banks.Accounts.Interfaces
{
    using Banks.Entities.Interfaces;

    public interface ICreditAccount
    {
        IClient Client { get; }
        decimal CreditLimit { get; }
        decimal Balance { get; }
        decimal FixedCommission { get; }
        void ApplyDailyCommission();
    }
}