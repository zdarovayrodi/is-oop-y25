namespace Banks.Entities.Interfaces;

public interface ICentralBank
{
    IBank CreateBank(string name, Dictionary<decimal, decimal> depositRates, decimal debitRate);
    void RewindTime(uint amountOfMonths);
}