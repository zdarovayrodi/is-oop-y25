namespace Banks.Entities
{
    using Banks.Entities.Interfaces;
    using Banks.Models;

    public class CentralBank : ICentralBank
    {
        private List<IBank> _banks = new List<IBank>();

        public IBank CreateBank(string name, List<DepositInterestRates> depositRates, decimal debitRate)
        {
            IBank bank = new Bank(name, depositRates, debitRate);
            _banks.Add(bank);
            return bank;
        }

        public void RewindTime(uint amountOfMonths)
        {
            for (int i = 0; i < amountOfMonths; i++)
            {
                foreach (var bank in _banks)
                {
                    // TODO: placeholder for bank's monthly operations
                }
            }
        }
    }
}