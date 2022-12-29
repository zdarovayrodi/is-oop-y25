using Spectre.Console;

namespace PresentationLayer
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            while (true)
            {
                var menu = new Menu()
                    .Add("Register an account", () => RegisterAnAccount())
                    .Add("Log in", () => Authentification())
                    .Add("Exit", () => Environment.Exit(0));

                AnsiConsole.Render(menu);
                AnsiConsole.MarkupLine("Choose an option [bold green]from the menu[/] above");
                AnsiConsole.Prompt(menu);
            }
        }

        private static void RegisterAnAccount()
        {
            throw new NotImplementedException();
        }

        private static void Authentification()
        {
            // get user input for username and password
            var username = AnsiConsole.Ask<string>("Enter your username");
            var password = AnsiConsole.Ask<string>("Enter your password");
        }
    }

    public class Menu
    {
    }
}