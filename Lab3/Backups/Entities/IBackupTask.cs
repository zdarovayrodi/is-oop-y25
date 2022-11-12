namespace Backups.Entities
{
    using Backups.Models;

    public interface IBackupTask
    {
        IReadOnlyList<IRestorePoint> RestorePoints { get; }
        IStorage Storage { get; }
        string BackupName { get; }

        void AddBackupObject(IBackupObject backupObject);
        void AddBackupObjects(List<IBackupObject> backupObjectList);
        void RemoveBackupObject(IBackupObject backupObject);
        IRestorePoint CreateRestorePoint();
    }
}