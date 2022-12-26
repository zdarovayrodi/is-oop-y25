namespace Banks.Console
{
    using Banks.Entities;
    using Banks.Models;
    using Spectre.Console;

    public class Program
    {
        [Obsolete("Obsolete")]
        public static void Main(string[] args)
        {
            var centralBank = new CentralBank();

            while (true)
            {
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .AddChoices(new[] { "Central bank", "Create bank", "Create client", "Money operations" }));

                if (choice == "Central bank")
                {
                    var centralBankChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(new[] { "Show all banks", "Show all clients" }));

                    if (centralBankChoice == "Show all banks") ShowAllBanks(centralBank);
                    else if (centralBankChoice == "Show all clients") ShowAllClients(centralBank);
                }
                else if (choice == "Create bank")
                {
                    var bankName = AnsiConsole.Ask<string>("Enter bank name");
                    var lowDepositMoney = AnsiConsole.Ask<decimal>("Enter [green]low[/] deposit money");
                    var lowDepositInterestRates = AnsiConsole.Ask<decimal>("Enter [green]low[/] deposit interest rate");
                    var medDepositMoney = AnsiConsole.Ask<decimal>("Enter [yellow]medium[/] deposit money");
                    var medDepositInterestRates =
                        AnsiConsole.Ask<decimal>("Enter [yellow]medium[/] deposit interest rate");
                    var highDepositMoney = AnsiConsole.Ask<decimal>("Enter [red]high[/] deposit money");
                    var highDepositInterestRates =
                        AnsiConsole.Ask<decimal>("Enter [red]high[/] deposit interest rate");
                    var depositInterestRates = new List<DepositInterestRates>
                    {
                        new DepositInterestRates(lowDepositMoney, lowDepositInterestRates),
                        new DepositInterestRates(medDepositMoney, medDepositInterestRates),
                        new DepositInterestRates(highDepositMoney, highDepositInterestRates),
                    };
                    var debitRate = AnsiConsole.Ask<decimal>("Enter debit rate (for debit cards)");
                    var availbleSumIfSuspicious =
                        AnsiConsole.Ask<decimal>("Enter available sum for suspicious clients");
                    centralBank.CreateBank(bankName, depositInterestRates, debitRate, availbleSumIfSuspicious);
                }
                else if (choice == "Create client")
                {
                    var clientName = AnsiConsole.Ask<string>("Enter client name");
                    var clientSurname = AnsiConsole.Ask<string>("Enter client surname");
                    var clientAddress = AnsiConsole.Ask<string>("Enter client address");
                    var clientPassport = AnsiConsole.Ask<string>("Enter client passport");
                    centralBank.RegisterClient(clientName, clientSurname, clientAddress, clientPassport);
                }
                else if (choice == "Money operations")
                {
                    var moneyOperationChoice = AnsiConsole.Prompt(
                        new SelectionPrompt<string>()
                            .PageSize(10)
                            .AddChoices(new[] { "Deposit money", "Withdraw money", "Transfer money" }));
                }
            }
        }

        [Obsolete("Obsolete")]
        private static void ShowAllBanks(CentralBank centralBank)
        {
            var table = new Table();
            table.AddColumn("Name");
            table.AddColumn("Deposit interest rate");
            table.AddColumn("Debit rate");
            table.AddColumn("Available sum for suspicious clients");
            foreach (var bank in centralBank.Banks)
            {
                table.AddRow(bank.Name, bank.DebitInterestRate.ToString(), bank.AvaliableSumIfSuspicious.ToString());
            }

            AnsiConsole.Render(table);
        }

        [Obsolete("Obsolete")]
        private static void ShowAllClients(CentralBank centralBank)
        {
            var table = new Table();
            table.AddColumn("Name");
            table.AddColumn("Surname");
            table.AddColumn("Address");
            table.AddColumn("Passport");
            foreach (var client in centralBank.Clients)
            {
                table.AddRow(client.Name, client.Surname, client.Address, client.Passport);
            }

            AnsiConsole.Render(table);
        }
    }
}