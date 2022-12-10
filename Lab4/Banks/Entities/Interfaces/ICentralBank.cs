using Banks.Models;

namespace Banks.Entities.Interfaces;

public interface ICentralBank
{
    Bank CreateBank(string name, List<DepositInterestRates> depositRates, decimal debitRate, decimal avaliableSumIfSuspicious);
    void RewindTime(uint amountOfMonths);
}