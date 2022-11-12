namespace Backups.Entities
{
    using Backups.Exceptions;
    public class BackupObject : IBackupObject
    {
        public BackupObject(string fileName, string filePath)
        {
            if (string.IsNullOrEmpty(fileName)) throw new BackupException("file name is null");
            if (string.IsNullOrEmpty(filePath)) throw new BackupException("file path is null");

            FileName = fileName;
            FilePath = filePath;
        }

        public string FileName { get; }
        public string FilePath { get; }
        public string FullPath => FilePath + FileName;
    }
}