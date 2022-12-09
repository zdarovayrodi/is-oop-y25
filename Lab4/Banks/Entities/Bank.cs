using Banks.Models;

namespace Banks.Entities
{
    using Banks.Accounts;
    using Banks.Entities.Interfaces;
    using Banks.Exceptions;

    public class Bank : IBank
    {
        private List<IClient> _clients = new List<IClient>();
        private List<CreditAccount> _creditAccounts = new List<CreditAccount>();
        private List<DebitAccount> _debitAccounts = new List<DebitAccount>();
        private List<DepositAccount> _depositAccounts = new List<DepositAccount>();
        private List<DepositInterestRates> _depositInterestRates = new List<DepositInterestRates>();

        public Bank(string name, List<DepositInterestRates> depositRates, decimal debitRate)
        {
            if (string.IsNullOrEmpty(name))
                throw new BankException("Bank name cannot be null or empty");
            if (depositRates == null || depositRates.Count == 0)
                throw new BankException("Deposit rates cannot be null or empty");
            if (debitRate <= 0)
                throw new BankException("Debit rate cannot be less or equal to zero");

            Name = name;
            _depositInterestRates = depositRates;
            DebitInterestRate = debitRate;
        }

        public string Name { get; }

        public IReadOnlyList<DepositInterestRates> DepositInterestRates => _depositInterestRates.AsReadOnly();
        public decimal DebitInterestRate { get; }
        public IReadOnlyList<IClient> Clients => _clients.AsReadOnly();
        public IReadOnlyList<CreditAccount> CreditAccounts => _creditAccounts.AsReadOnly();
        public IReadOnlyList<DebitAccount> DebitAccounts => _debitAccounts.AsReadOnly();
        public IReadOnlyList<DepositAccount> DepositAccounts => _depositAccounts.AsReadOnly();

        public void AddClient(IClient client)
        {
            if (client == null)
                throw new BankException("Client is null");
            _clients.Add(client);

            // _debitAccounts.Add(DebitAccount(client, DebitInterestRate));
        }

        public void ApplyInterests()
        {
            foreach (DebitAccount account in _debitAccounts)
                account.ApplyInterest();
            foreach (DepositAccount account in _depositAccounts)
                account.ApplyMonthlyInterest();
        }

        public void CalculateDailyDebitAccountsInterest()
        {
            foreach (var account in _debitAccounts)
            {
                account.CalculateDailyInterest();
            }
        }

        public void CalculateDailyCreditAccountsInterest()
        {
            foreach (var account in _creditAccounts)
            {
                account.ApplyDailyInterest();
            }
        }
    }
}