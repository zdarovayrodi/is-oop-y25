namespace Backups.Models
{
    public class Storage : IStorage
    {
        public Storage(string originalPath, string backupPath, byte[] bytes)
        {
            OriginalPath = originalPath;
            BackupPath = backupPath;
            Bytes = bytes;
        }

        public string OriginalPath { get; }
        public string BackupPath { get; }
        public byte[] Bytes { get; }
    }
}