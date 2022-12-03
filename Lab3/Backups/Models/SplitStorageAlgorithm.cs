namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SplitStorageAlgorithm : IAlgorithm
    {
        public void Compress(IBackupTask backupTask, IRestorePoint restorePoint, int id, IRepository repository)
        {
            string zipDir = backupTask.FullPath;
            repository.CreateDirectory(zipDir);
            string zipRestorePointDir = Path.Combine(zipDir, restorePoint.Name);
            repository.CreateDirectory(zipRestorePointDir);
            foreach (var backupObject in restorePoint.BackupObjects)
            {
                string zipPath = Path.Combine(zipRestorePointDir, $"{backupObject.Name}_{id}.zip");
                repository.OpenArchive(zipPath);
                repository.CreateEntryFromFile(backupObject.FullPath, backupObject.Name);
                repository.Dispose();
                var storage = new Storage(backupObject.FullPath, zipPath, File.ReadAllBytes(backupObject.FullPath));
                restorePoint.AddStorage(storage);
            }
        }
    }
}