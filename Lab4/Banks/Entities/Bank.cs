using Banks.Models;

namespace Banks.Entities
{
    using Banks.Accounts;
    using Banks.Entities.Interfaces;
    using Banks.Exceptions;
    using Banks.Observer;

    public class Bank : IObservable
    {
        private List<Client> _clients = new List<Client>();
        private List<CreditAccount> _creditAccounts = new List<CreditAccount>();
        private List<DebitAccount> _debitAccounts = new List<DebitAccount>();
        private List<DepositAccount> _depositAccounts = new List<DepositAccount>();
        private List<DepositInterestRates> _depositInterestRates;
        private List<IObserver> _observers = new List<IObserver>();

        public Bank(string name, List<DepositInterestRates> depositRates, decimal debitRate, decimal avaliableSumIfSuspicious)
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
            AvaliableSumIfSuspicious = avaliableSumIfSuspicious;
        }

        public string Name { get; }

        public IReadOnlyList<DepositInterestRates> DepositInterestRates => _depositInterestRates.AsReadOnly();
        public decimal DebitInterestRate { get; }
        public IReadOnlyList<Client> Clients => _clients.AsReadOnly();
        public IReadOnlyList<CreditAccount> CreditAccounts => _creditAccounts.AsReadOnly();
        public IReadOnlyList<DebitAccount> DebitAccounts => _debitAccounts.AsReadOnly();
        public IReadOnlyList<DepositAccount> DepositAccounts => _depositAccounts.AsReadOnly();
        public decimal AvaliableSumIfSuspicious { get; }

        public void AddClient(Client client)
        {
            if (client == null)
                throw new BankException("Client is null");
            if (_clients.Contains(client))
                throw new BankException("Client already exists");
            _clients.Add(client);

            _debitAccounts.Add(new DebitAccount(client, DebitInterestRate, AvaliableSumIfSuspicious));
        }

        public CreditAccount CreateCreditAccount(Client client, decimal creditLimit, decimal fixedInterestRate)
        {
            if (client == null)
                throw new BankException("Client is null");
            if (creditLimit <= 0)
                throw new BankException("Credit limit cannot be less or equal to zero");
            if (!_clients.Contains(client))
                throw new BankException("Client does not exist");

            var creditAccount = new CreditAccount(client, creditLimit, fixedInterestRate, AvaliableSumIfSuspicious);
            _creditAccounts.Add(creditAccount);
            return creditAccount;
        }

        public DepositAccount CreateDepositAccount(Client client, decimal balance, DateOnly endDate)
        {
            if (client == null)
                throw new BankException("Client is null");
            if (balance <= 0)
                throw new BankException("Amount cannot be less or equal to zero");
            if (!_clients.Contains(client))
                throw new BankException("Client does not exist");

            var depositAccount = new DepositAccount(client, DepositInterestRates, balance, endDate, AvaliableSumIfSuspicious);
            _depositAccounts.Add(depositAccount);
            return depositAccount;
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

        public void AddObserver(IObserver observer)
        {
            if (observer == null)
                throw new ObserverException("Observer is null");
            if (_observers.Contains(observer))
                throw new ObserverException("Observer already exists");
            _observers.Add(observer);
        }

        public void NotifyObservers(string message)
        {
            foreach (var observer in _observers)
            {
                observer.Update(message);
            }
        }
    }
}