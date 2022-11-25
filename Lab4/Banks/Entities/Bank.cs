namespace Banks.Entities
{
    using Banks.Accounts;
    using Banks.Entities.Interface;
    using Banks.Exceptions;

    public class Bank : IBank
    {
        private List<IClient> _clients = new List<IClient>();
        private List<CreditAccount> _creditAccounts = new List<CreditAccount>();
        private List<DebitAccount> _debitAccounts = new List<DebitAccount>();
        private List<DepositAccount> _depositAccounts = new List<DepositAccount>();
        private Dictionary<decimal, decimal> _depositInterestRates = new Dictionary<decimal, decimal>();

        public Bank(string name, Dictionary<decimal, decimal> depositRates, decimal debitRate)
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

        public IReadOnlyDictionary<decimal, decimal> DepositInterestRates => _depositInterestRates;
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
    }
}