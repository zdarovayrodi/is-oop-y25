namespace Backups.Models;

public interface IStorage
{
    string OriginalPath { get; }
    string BackupPath { get; }
}