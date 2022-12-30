using Lab6.Entities;

namespace Lab6.Models;

public class Report
{
    private readonly List<Message>? _messages;
    public Report(List<Message> messages)
    {
        _messages = messages;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; set; }
    public uint CountOfProcessedMessages { get; set; }
    public IReadOnlyList<Message> Messages { get; set; }
    public int GetProcessedMessagesCount()
        => Messages.Count(msg => msg.IsProcessed);
    public int GetMessagesCount(Account processedFrom)
        => Messages.Count(msg => msg.ProcessedFrom == processedFrom);
    public int GetMessagesCount(DateTime start, DateTime end)
        => Messages.Count(msg => msg.TimeStamp >= start && msg.TimeStamp <= end);
}