namespace Backups.Models
{
    using Backups.Entities;

    public interface IAlgorithm
    {
        void Compress(IBackupTask backupTask, IRestorePoint restorePoint, int id);
    }
}