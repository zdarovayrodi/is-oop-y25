namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SingleStorageAlgorithm : IAlgorithm
    {
        public void Compress(IBackupTask backupTask, IRestorePoint restorePoint, int id)
        {
            string zipDir = Directory.CreateDirectory(backupTask.FullPath).FullName;
            string zipPath = Path.Combine(zipDir, $"{restorePoint.Name}_{id}.zip");
            ZipArchive zipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
            foreach (var backupObject in restorePoint.BackupObjects)
            {
                zipArchive.CreateEntryFromFile(backupObject.FullPath, $"{backupObject.Name}_{id}.{backupObject.Extension}");
                var storage = new Storage(backupObject.FullPath, zipPath, File.ReadAllBytes(backupObject.FullPath));
                restorePoint.AddStorage(storage);
            }

            zipArchive.Dispose();
        }
    }
}