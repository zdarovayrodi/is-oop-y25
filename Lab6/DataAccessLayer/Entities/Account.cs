using Lab6.Models;

namespace Lab6.Entities;

public class Account
{
    private string _password;
    public Account(string username, string password, AccountStatus status)
    {
        Username = username;
        _password = password;
        Status = status;
    }
    
    public string Username { get; set; }
    public string? Email { get; set; }
    public string? Messenger { get; set; }
    public AccountStatus Status { get; set; }
    public bool IsCorrectPassword(string password) => _password == password;
}
