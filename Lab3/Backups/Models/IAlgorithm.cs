namespace Backups.Models
{
    using Backups.Entities;

    public interface IAlgorithm
    {
        byte[] Compress(IBackupTask backupTask, IRestorePoint restorePoint, int id);
    }
}