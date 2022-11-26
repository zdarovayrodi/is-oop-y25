namespace Backups.Entities
{
    using Backups.Models;

    public interface IBackupTask
    {
        IReadOnlyList<IRestorePoint> RestorePoints { get; }
        IReadOnlyList<IStorage> Storages { get; }
        IAlgorithm Algorithm { get; }
        string Name { get; }
        string Path { get; }
        void AddBackupObject(IBackupObject backupObject);
        void AddBackupObjects(List<IBackupObject> backupObjectList);
        void RemoveBackupObject(IBackupObject backupObject);
        void AddStorage(IStorage storage);
        IRestorePoint CreateRestorePoint();
    }
}