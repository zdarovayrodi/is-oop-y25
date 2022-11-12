namespace Backups.Entities;

public interface IBackupObject
{
    string FileName { get; }
    string FilePath { get; }
    string FullPath { get; }
}