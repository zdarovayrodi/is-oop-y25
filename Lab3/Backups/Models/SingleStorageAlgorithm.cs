namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SingleStorageAlgorithm : IAlgorithm
    {
        public void SaveFiles(IBackupTask backupTask, IRestorePoint restorePoint, int id)
        {
            if (restorePoint is null) throw new StorageException("restore Point is null");
            string backupFolderFullPath = backupTask.BackupFullPath;
            if (!Directory.Exists(backupFolderFullPath))
                Directory.CreateDirectory(backupFolderFullPath);
            if (!Directory.Exists(backupFolderFullPath + restorePoint.Name))
                Directory.CreateDirectory(backupFolderFullPath + restorePoint.Name);

            string zipPath = backupFolderFullPath + restorePoint.Name + ".zip";
            using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
            {
                foreach (var file in restorePoint.BackupObjects)
                {
                    string fileName = file.Name + " " + id;
                    if (file.Extension != string.Empty) fileName += "." + file.Extension;
                    archive.CreateEntryFromFile(file.Path, fileName);
                }
            }

            Directory.Delete(backupFolderFullPath + restorePoint.Name, true);
            AddStorage(backupTask, new Storage(zipPath));
        }

        public void AddStorage(IBackupTask backupTask, IStorage storage)
        {
            backupTask.AddStorage(storage);
        }
    }
}