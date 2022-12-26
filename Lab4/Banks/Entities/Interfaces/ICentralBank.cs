using Banks.Models;

namespace Banks.Entities.Interfaces;

public interface ICentralBank
{
    IReadOnlyList<Bank> Banks { get; }
    IReadOnlyList<Client> Clients { get; }
    Client RegisterClient(string name, string surname, string passportNumber, string address);
    Bank CreateBank(string name, List<DepositInterestRates> depositRates, decimal debitRate, decimal avaliableSumIfSuspicious);
    void RewindTime(uint amountOfMonths);
}