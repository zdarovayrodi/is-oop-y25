namespace Banks.Entities.Interface
{
    using Banks.Accounts;

    public interface IBank
    {
        string Name { get; }
        IReadOnlyList<IClient> Clients { get; }
        IReadOnlyDictionary<decimal, decimal> DepositInterestRates { get; }
        IReadOnlyList<CreditAccount> CreditAccounts { get; }
        IReadOnlyList<DebitAccount> DebitAccounts { get; }
        IReadOnlyList<DepositAccount> DepositAccounts { get; }
        void AddClient(IClient client);
    }
}