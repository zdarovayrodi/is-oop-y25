using Banks.Models;

namespace Banks.Entities.Interfaces
{
    using Banks.Accounts;

    public interface IBank
    {
        string Name { get; }
        IReadOnlyList<IClient> Clients { get; }
        IReadOnlyList<DepositInterestRates> DepositInterestRates { get; }
        IReadOnlyList<CreditAccount> CreditAccounts { get; }
        IReadOnlyList<DebitAccount> DebitAccounts { get; }
        IReadOnlyList<DepositAccount> DepositAccounts { get; }
        void AddClient(IClient client);
    }
}