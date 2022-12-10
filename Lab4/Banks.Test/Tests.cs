using Banks.Models;

namespace Banks.Test
{
    using Banks.Entities;
    using Banks.Entities.Interfaces;
    using Xunit;

    public class Tests
    {
        private ICentralBank centralBank = new CentralBank();

        [Fact]
        public void Test()
        {
            var lowInterest = new DepositInterestRates(10000, 10);
            var medInterest = new DepositInterestRates(100000, 15);
            var highInterest = new DepositInterestRates(1000000, 20);
            List<DepositInterestRates> ratesList = new List<DepositInterestRates>();
            ratesList.Add(lowInterest);
            ratesList.Add(medInterest);
            ratesList.Add(highInterest);
            var debitRate = 10;
            var testBank1 = centralBank.CreateBank("Test Bank 1", ratesList, debitRate, 10000);
            var testClient1 = new Client("Test", "Testoviy");
            testBank1.AddClient(testClient1);
            Assert.Contains(testClient1, testBank1.Clients);
            Assert.Equal(testClient1, testBank1.DebitAccounts.First().Client);
            Assert.Empty(testBank1.DepositAccounts);
            Assert.Empty(testBank1.CreditAccounts);
        }

        [Fact]
        public void TestRevertTransaction()
        {
            var lowInterest = new DepositInterestRates(10000, 10);
            var medInterest = new DepositInterestRates(100000, 15);
            var highInterest = new DepositInterestRates(1000000, 20);
            List<DepositInterestRates> ratesList = new List<DepositInterestRates>();
            ratesList.Add(lowInterest);
            ratesList.Add(medInterest);
            ratesList.Add(highInterest);
            var debitRate = 10;
            var testBank1 = centralBank.CreateBank("Test Bank 1", ratesList, debitRate, 10000);
            var testClient1 = new Client("Test", "Testoviy");
            var testClient2 = new Client("Test2", "Testoviy");
            testBank1.AddClient(testClient1);
            testBank1.AddClient(testClient2);
            var testAccount1 = testBank1.DebitAccounts.First();
            var testAccount2 = testBank1.DebitAccounts.Last();
            testAccount1.Deposit(1000);
            Assert.Equal(1000, testAccount1.Balance);
            var transaction = testAccount1.Transfer(testAccount2, 500);
            Assert.Equal(500, testAccount1.Balance);
            Assert.Equal(500, testAccount2.Balance);
            testAccount1.RevertTransaction(transaction);
            Assert.Equal(1000, testAccount1.Balance);
        }
    }
}