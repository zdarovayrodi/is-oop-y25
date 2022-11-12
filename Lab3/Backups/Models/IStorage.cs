namespace Backups.Models
{
    using Backups.Entities;

    public interface IStorage
    {
        void SaveFiles(IBackupTask backupTask, IRestorePoint restorePoint);
    }
}