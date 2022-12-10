namespace Banks.Entities
{
    using Banks.Entities.Interfaces;
    using Banks.Models;

    public class CentralBank : ICentralBank
    {
        private List<Bank> _banks = new List<Bank>();

        public Bank CreateBank(string name, List<DepositInterestRates> depositRates, decimal debitRate, decimal avaliableSumIfSuspicious)
        {
            Bank bank = new Bank(name, depositRates, debitRate, avaliableSumIfSuspicious);
            _banks.Add(bank);
            return bank;
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