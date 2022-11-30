namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SplitStorageAlgorithm : IAlgorithm
    {
        public void Compress(IBackupTask backupTask, IRestorePoint restorePoint, int id)
        {
            string zipDir = Directory.CreateDirectory(backupTask.FullPath).FullName;
            string zipRestorePointDir = Directory.CreateDirectory(Path.Combine(zipDir, restorePoint.Name)).FullName;
            foreach (var backupObject in restorePoint.BackupObjects)
            {
                string zipPath = Path.Combine(zipRestorePointDir, $"{backupObject.Name}_{id}.zip");
                ZipArchive zipArchive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
                zipArchive.CreateEntryFromFile(backupObject.FullPath, backupObject.Name);
                zipArchive.Dispose();
                var storage = new Storage(backupObject.FullPath, zipPath, File.ReadAllBytes(backupObject.FullPath));
                restorePoint.AddStorage(storage);
            }
        }
    }
}