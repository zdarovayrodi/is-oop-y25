using Lab6.Models;

namespace Lab6.Entities;

public class Account
{
    private string _password;
    private List<Account> _subordinates = new List<Account>(); 
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
    public IReadOnlyList<Account> Subordinates => _subordinates.AsReadOnly();
    public bool IsCorrectPassword(string password) => _password == password;
    
    public void AddSubordinate(Account subordinate)
    {
        if (subordinate == null) throw new Exception("Account is null");
        _subordinates.Add(subordinate);
    }
}
