using BusinessLayer;
using BusinessLayer.Handlers;
using BusinessLayer;
using BusinessLayer.Service;
using Lab6.Entities;
using Lab6.Models;
using Spectre.Console;

namespace PresentationLayer
{
    public class Program
    {
        private static MessageService _service;
        private static Account _account;
        private static List<Report> _reports;
        private static List<string> _choices = new List<string>
        {
            "Register an account",
            "Exit",
        };
        private static Message? _message;

        public static MessageService Service
        {
            get => _service;
            set => _service = value;
        }
        
        public static Account Account
        {
            get => _account;
            set => _account = value;
        }
        public static void Main(string[] args)
        {
            while (true)
            {
                AnsiConsole.Clear();
                
                var employeeChoices = new List<string>
                {
                    "View message",
                };
                
                var bossChoices = new List<string>
                {
                    "Create report",
                };

                _choices.Insert(_choices.Count - 1, _account != null ? "Log out" : "Log in");

                var menu = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .PageSize(10)
                        .AddChoices(_choices));
                
                switch (menu) {
                    case "Register an account":
                        RegisterAnAccount();
                        break;
                    case "Log in":
                        Authentification();
                        break;
                    case "Log out":
                        _account = null;
                        AnsiConsole.MarkupLine("[red]You have been logged out[/]");
                        break;
                    case "View message":
                        ViewMessage(_account);
                        break;
                    case "Mark as processed":
                        MarkAsProcessed(_message, _account);
                        break;
                    case "Answer this message":
                        AnsiConsole.Prompt(new TextPrompt<string>("Enter your reply"));
                        AnsiConsole.MarkupLine("[green]Your reply has been sent successfully![/]");
                        break;
                    case "Create report":
                        _reports.Add(_service.CreateReport(_account));
                        break;
                    case "Exit":
                        return;
                }
            }
        }

        private static void MarkAsProcessed(Message message, Account account)
        {
            _service.ProcessMessage(message, account);
        }

        private static void ViewMessage(Account account)
        {
            _message = _service.GetEarliestUnprocessedMessage();
            if (_message == null)
            {
                AnsiConsole.MarkupLine("[red]No message to process[/]");
                return;
            }
            AnsiConsole.MarkupLine($"[green]Message received from {_message.From}[/]");
            AnsiConsole.MarkupLine($"[green]Date: {_message.TimeStamp}[/]");
            AnsiConsole.MarkupLine($"[yellow]{_message.Text}[/]");
            AnsiConsole.MarkupLine("[green]Do you want to process this message?[/]");
            _choices.Insert(
                _choices.Count - 1,
                "Answer this message");
            _choices.Insert(
                _choices.Count - 1,
                "Mark as processed");

        }

        private static void RegisterAnAccount()
        {
            var username = AnsiConsole.Ask<string>("Enter your username");
            var password = AnsiConsole.Ask<string>("Enter your password");
            var confirmPassword = AnsiConsole.Ask<string>("Confirm your password");
            if (password != confirmPassword)
            {
                AnsiConsole.MarkupLine("[red]Passwords do not match[/]");
                return;
            }
            string userStatus = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Select your status")
                    .PageSize(3)
                    .AddChoices("Boss", "Employee", "User"));
            AccountStatus status = AccountStatus.User;
            switch (userStatus)
            {
                case "Boss":
                    status = AccountStatus.Boss;
                    break;
                case "Employee":
                    status = AccountStatus.Employee;
                    break;
                case "User":
                    status = AccountStatus.User;
                    break;
            }
            var account = new Account
            (
                 username,
                 password,
                 status
            );
            _service.AddAccount(account);
            AnsiConsole.MarkupLine("[green]Account created successfully[/]");
            
        }

        private static void Authentification()
        {
            var username = AnsiConsole.Ask<string>("Enter your username");
            var password = AnsiConsole.Ask<string>("Enter your password");
            
            if (AuthentificationHandler.Authentification(_service, username, password))
            {
                AnsiConsole.MarkupLine("[green]You are logged in[/]");
                _account = _service.GetAccount(username); 
            }
            else
            {
                AnsiConsole.MarkupLine("[red]Wrong username or password[/]");
            }
        }
    }
}