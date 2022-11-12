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
            string backupFolderFullPath = backupTask.BackupFullPath + backupTask.BackupName;
            if (!Directory.Exists(backupFolderFullPath)) Directory.CreateDirectory(backupFolderFullPath);
            var id = Guid.NewGuid();
            string zipPath = backupFolderFullPath + restorePoint.Name + id;
            if (!Directory.Exists(zipPath)) Directory.CreateDirectory(zipPath);

            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (var file in restorePoint.BackupObjects)
                {
                    archive.CreateEntryFromFile(file.FileName, file.FileName);
                }
            }
        }
    }
}