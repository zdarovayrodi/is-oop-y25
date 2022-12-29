using Lab6.DataAccessLayer.Models;

namespace Lab6.DataAccessLayer.Entities;

public class Account
{
    private string _password;
    public Account(string name, string password, AccountStatus status)
    {
        Name = name;
        _password = password;
        Status = status;
    }
    
    public string Name { get; set; }
    public string Password => _password;
    public string Email { get; set; }
    public string Messenger { get; set; }
    public AccountStatus Status { get; set; }
}