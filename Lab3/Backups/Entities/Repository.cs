namespace Backups.Entities
{
    using Backups.Exceptions;

    public class Repository : IRepository
    {
        public byte[] GetFile(string filePath)
        {
            if (!File.Exists(filePath))
                throw new BackupException("Path not found");
            return File.ReadAllBytes(filePath);
        }

        public void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public void Write(string path, byte[] data)
        {
            File.WriteAllBytes(path, data);
        }
    }
}