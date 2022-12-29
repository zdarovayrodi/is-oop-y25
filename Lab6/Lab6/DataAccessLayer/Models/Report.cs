using Lab6.DataAccessLayer.Entities;

namespace Lab6.DataAccessLayer.Models;

public class Report
{
    public Report() { Id = Guid.NewGuid(); }
    public Guid Id { get; set; }
    public uint CountOfProcessedMessages { get; set; }
    public IReadOnlyList<Message> Messages { get; set; }
    public int GetProcessedMessegasCount() => Messages.Count(msg => msg.IsProcessed);
    public int GetMessagesCount(Device device) => Messages.Count(msg => msg.Device == device);
    public int GetMessagesCount(DateTime start, DateTime end) => Messages.Count(msg => msg.DateTime >= start && msg.DateTime <= end);
}