using BusinessLayer.Exceptions;
using BusinessLayer.Service;
using Lab6.Entities;
using Lab6.Models;
using Xunit;

namespace BusinessLayer;

public class ServiceTests
{
    [Fact]
    public void CanCreateAccount()
    {
        var service = new MessageService();
        var employee1 = new Account("John", "1", AccountStatus.Employee);
        var employee2 = new Account("Jane", "11", AccountStatus.Employee); 
        var boss = new Account("Boss", "111", AccountStatus.Boss);
        service.AddAccount(employee1);
        service.AddAccount(employee2);
        service.AddAccount(boss);
        Assert.Equal(3, service.Accounts.Count);
        employee1.AddSubordinate(employee2);
        Assert.Equal(1, employee1.Subordinates.Count);
    }

    [Fact]
    public void CanMarkMessagesAsProcessed()
    {
        
        var service = new MessageService();
        var employee1 = new Account("John", "1", AccountStatus.Employee);
        var user1 = new Account("User1", "11", AccountStatus.User);
        service.AddAccount(employee1);
        service.AddAccount(user1);
        var msg1 = new Message("Text", DateTime.Now, user1);
        service.AddMessage(msg1);
        Assert.Equal(1, service.Messages.Count);
        Assert.Equal(0, service.Messages.Count(m => m.IsProcessed));
        service.ProcessMessage(msg1, employee1);
        Assert.Equal(1, service.Messages.Count(m => m.IsProcessed));
        Assert.Equal(employee1, msg1.ProcessedFrom);
    }

    [Fact]
    public void CanCreateReport()
    {
        var service = new MessageService();
        var employee1 = new Account("John", "1", AccountStatus.Employee);
        var user1 = new Account("User1", "11", AccountStatus.User);
        var boss = new Account("Boss", "111", AccountStatus.Boss);
        service.AddAccount(employee1);
        service.AddAccount(user1);
        service.AddAccount(boss);
        var msg1 = new Message("Text", DateTime.Now, user1);
        service.AddMessage(msg1);
        service.ProcessMessage(msg1, employee1);
        Assert.Throws<ReportException>(() => service.CreateReport(employee1));
        var report1 = service.CreateReport(boss);
        Assert.Equal(1, service.Reports.Count);
    }
}