namespace Backups.Models
{
    using System.IO.Compression;
    using Backups.Entities;
    using Backups.Exceptions;

    public class SplitStorageAlgorithm : IAlgorithm
    {
        public void SaveFiles(IBackupTask backupTask, IRestorePoint restorePoint, int id)
        {
            if (restorePoint is null) throw new StorageException("restore Point is null");
            string backupFolderFullPath = backupTask.Path;

            if (!Directory.Exists(backupFolderFullPath))
                Directory.CreateDirectory(backupFolderFullPath);
            if (!Directory.Exists(backupFolderFullPath + restorePoint.Name))
                Directory.CreateDirectory(Path.Join(backupFolderFullPath, restorePoint.Name));

            foreach (IBackupObject file in restorePoint.BackupObjects)
            {
                string zipPath = Path.Join(backupFolderFullPath, restorePoint.Name, file.Name + " " + id + ".zip");
                using (ZipArchive archive = ZipFile.Open(zipPath, ZipArchiveMode.Create))
                {
                    string fileName = file.Name + " " + id;
                    if (file.Extension != string.Empty) fileName += "." + file.Extension;
                    archive.CreateEntryFromFile(file.Path, fileName);
                }

                // string dirPath = backupFolderFullPath + restorePoint.Name;
                // if (Directory.Exists(dirPath))
                // {
                //     Directory.Delete(dirPath, true);
                // }
                AddStorage(backupTask, new Storage(zipPath));
            }
        }

        public void AddStorage(IBackupTask backupTask, IStorage storage)
        {
            backupTask.AddStorage(storage);
        }
    }
}