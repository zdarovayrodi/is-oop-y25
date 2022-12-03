namespace Banks.Entities
{
    using Banks.Entities.Interfaces;

    public class CentralBank : ICentralBank
    {
        private List<IBank> _banks = new List<IBank>();

        public IBank CreateBank(string name, Dictionary<decimal, decimal> depositRates, decimal debitRate)
        {
            IBank bank = new Bank(name, depositRates, debitRate);
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