namespace Backups.Entities;

public interface IBackupObject
{
    string Name { get; }
    string Extension { get; }
    string FullPath { get; }
    byte[] FileBytes { get; }
}