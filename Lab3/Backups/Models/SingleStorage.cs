namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SingleStorage : IStorage
    {
        public void SaveFiles(IBackupTask backupTask, IRestorePoint restorePoint)
        {
            if (restorePoint is null) throw new StorageException("restore Point is null");
            string storageName = backupTask.BackupName;

            if (!Directory.Exists(storageName)) Directory.CreateDirectory(storageName);
            string zipPath = storageName + "\\" + restorePoint.Name + Guid.NewGuid() + ".zip";

            using ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create);
            foreach (var file in restorePoint.BackupObjects)
            {
                archive.CreateEntryFromFile(file.FullPath, file.FileName);
            }

            archive.Dispose();
        }
    }
}