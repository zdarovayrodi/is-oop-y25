using Spectre.Console;
using Spectre.Console.Rendering;

namespace PresentationLayer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                AnsiConsole.Clear();
                
                var menu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .AddChoices(new[]
                            { "Register an account", "Log in", "Exit" }));
                
                switch (menu) {
                    case "Register an account":
                        RegisterAnAccount();
                        break;
                    case "Log in":
                        Authentification();
                        break;
                    case "Exit":
                        return;
                }
            }
        }

        private static void RegisterAnAccount()
        {
            var username = AnsiConsole.Ask<string>("Enter your username");
            var password = AnsiConsole.Ask<string>("Enter your password");
            var confirmPassword = AnsiConsole.Ask<string>("Confirm your password");
            if (password != confirmPassword)
            {
                AnsiConsole.MarkupLine("[red]Passwords do not match[/]");
            }
        }

        private static void Authentification()
        {
            var username = AnsiConsole.Ask<string>("Enter your username");
            var password = AnsiConsole.Ask<string>("Enter your password");
        }
    }
}