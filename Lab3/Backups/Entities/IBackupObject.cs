namespace Backups.Entities;

public interface IBackupObject
{
    string Name { get; }
    string Path { get; }
}