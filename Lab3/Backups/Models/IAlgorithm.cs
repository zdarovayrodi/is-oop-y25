namespace Backups.Models
{
    using Backups.Entities;

    public interface IAlgorithm
    {
        void SaveFiles(IBackupTask backupTask, IRestorePoint restorePoint, int id);
        void AddStorage(IBackupTask backupTask, IStorage storage);
    }
}