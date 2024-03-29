namespace Backups.Entities
{
    using Backups.Models;

    public interface IBackupTask
    {
        IReadOnlyList<IRestorePoint> RestorePoints { get; }
        IAlgorithm Algorithm { get; }
        string Name { get; }
        string FullPath { get; }
        void AddBackupObject(IBackupObject backupObject);
        void AddBackupObjects(List<IBackupObject> backupObjectList);
        void RemoveBackupObject(IBackupObject backupObject);
        IRestorePoint CreateRestorePoint(string restorePointName);
        IRestorePoint AddRestorePoint(IRestorePoint restorePoint);
        void DeleteRestorePoint(IRestorePoint restorePoint);
    }
}