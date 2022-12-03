namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SingleStorageAlgorithm : IAlgorithm
    {
        public void Compress(IBackupTask backupTask, IRestorePoint restorePoint, int id, IRepository repository)
        {
            string zipDir = backupTask.FullPath;
            repository.CreateDirectory(zipDir);
            string zipPath = Path.Combine(zipDir, $"{restorePoint.Name}_{id}.zip");
            repository.OpenArchive(zipPath);
            foreach (var backupObject in restorePoint.BackupObjects)
            {
                repository.CreateEntryFromFile(backupObject.FullPath, $"{backupObject.Name}_{id}.{backupObject.Extension}");
                var storage = new Storage(backupObject.FullPath, zipPath, File.ReadAllBytes(backupObject.FullPath));
                restorePoint.AddStorage(storage);
            }

            repository.Dispose();
        }
    }
}