namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SplitStorage : IStorage
    {
        public void SaveFiles(IBackupTask backupTask, IRestorePoint restorePoint)
        {
            if (restorePoint is null) throw new StorageException("restore Point is null");
            string storageName = backupTask.BackupName;

            if (!Directory.Exists(storageName)) Directory.CreateDirectory(storageName);
            var id = Guid.NewGuid();
            foreach (var file in restorePoint.BackupObjects)
            {
                string zipPath = storageName + "\\" + file.Name + id + ".zip";
                using ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
                archive.CreateEntryFromFile(file.Path, file.Name);
                archive.Dispose();
            }
        }
    }
}