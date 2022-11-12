namespace Backups.Models
{
    using Backups.Entities;

    public interface IRestorePoint
    {
        DateTime CreationDate { get; }
        string Name { get; }
        IReadOnlyList<IBackupObject> BackupObjects { get; }
    }
}