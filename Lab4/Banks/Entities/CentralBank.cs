namespace Banks.Entities
{
    using Banks.Entities.Interfaces;
    using Banks.Models;

    public class CentralBank : ICentralBank
    {
        private List<Bank> _banks = new List<Bank>();
        private List<Client> _clients = new List<Client>();
        public IReadOnlyList<Bank> Banks => _banks.AsReadOnly();
        public IReadOnlyList<Client> Clients => _clients.AsReadOnly();

        public Bank CreateBank(string name, List<DepositInterestRates> depositRates, decimal debitRate, decimal avaliableSumIfSuspicious)
        {
            Bank bank = new Bank(name, depositRates, debitRate, avaliableSumIfSuspicious);
            _banks.Add(bank);
            return bank;
        }

        public Client RegisterClient(string name, string surname, string passportNumber, string address)
        {
            Client client = new Client(name, surname, address, passportNumber);
            _clients.Add(client);
            return client;
        }

        public void RewindTime(uint amountOfMonths)
        {
            for (int i = 0; i < amountOfMonths; i++)
            {
                foreach (var bank in _banks)
                {
                    for (int j = 0; j < 30; j++)
                    {
                        bank.CalculateDailyDebitAccountsInterest();
                        bank.CalculateDailyCreditAccountsInterest();
                    }

                    bank.ApplyInterests();
                }
            }
        }
    }
}