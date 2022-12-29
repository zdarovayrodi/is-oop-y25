using Lab6.Models;

namespace Lab6.Entities;

public class Message
{
    public Message(string text, DateTime date, Account from)
    {
        Text = text;
        TimeStamp = date;
        From = from;
        Id = Guid.NewGuid();
    }
    public Guid Id { get; }
    public Account From { get; set; }
    public string Text { get; set; }
    public bool IsProcessed { get; set; }
    public DateTime TimeStamp { get; set; }
    public MessageSource SourceFrom { get; private set; }
    public MessageSource ProcessedFrom { get; private set; }
}