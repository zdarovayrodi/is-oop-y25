namespace Backups.Models
{
    public class Storage : IStorage
    {
        public Storage(string originalPath, string backupPath, byte[] bytes)
        {
            if (!Directory.Exists(originalPath) && !File.Exists(originalPath)) throw new ArgumentException("Path is not valid");
            OriginalPath = originalPath;
            BackupPath = backupPath;
            Bytes = bytes;
        }

        public string OriginalPath { get; }
        public string BackupPath { get; }
        public byte[] Bytes { get; }
    }
}