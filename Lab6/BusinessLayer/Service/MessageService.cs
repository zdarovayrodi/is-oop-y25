using BusinessLayer.Exceptions;
using Lab6.Entities;
using Lab6.Models;

namespace BusinessLayer.Service;

public class MessageService
{
    private List<Account> _accounts = new List<Account>();
    private List<Message> _messages = new List<Message>();
    
    public IReadOnlyList<Account> Accounts => _accounts.AsReadOnly();
    public IReadOnlyList<Message> Messages => _messages.AsReadOnly();
    
    public Account GetAccount(string username)
    {
        return _accounts.FirstOrDefault(a => a.Username == username);
    }
    
    public void AddAccount(Account account)
    {
        if (account == null) throw new AccountException("Account is null");
        _accounts.Add(account);
    }
    
    public void AddMessage(Message message)
    {
        if (message == null) throw new MessageException("Message is null");
        _messages.Add(message);
    }

    public Message GetEarliestUnprocessedMessage()
    {
        return _messages.OrderBy(m => m.TimeStamp).FirstOrDefault(m => !m.IsProcessed);
    }
    
    public void ProcessMessage(Message message, Account account)
    {
        if (message == null) throw new MessageException("Message is null");
        message.IsProcessed = true;
        message.ProcessedFrom = account;
    }

    public Report CreateReport()
    {
        var report = new Report(_messages);
        _messages = _messages.Where(m => !m.IsProcessed).ToList();
        return report;
    }
}