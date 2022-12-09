using Banks.Models;

namespace Banks.Entities.Interfaces;

public interface ICentralBank
{
    IBank CreateBank(string name, List<DepositInterestRates> depositRates, decimal debitRate);
    void RewindTime(uint amountOfMonths);
}