using System.IO.Compression;

namespace Backups.Entities
{
    using Backups.Exceptions;

    public class Repository : IRepository
    {
        private ZipArchive? _zipArchive = null;
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

        public ZipArchive OpenArchive(string path)
        {
            ZipArchive zipArchive = ZipFile.Open(path, ZipArchiveMode.Create);
            _zipArchive = zipArchive;
            return zipArchive;
        }

        public void CreateEntryFromFile(string path, string entryName)
        {
            _zipArchive?.CreateEntryFromFile(path, entryName);
        }

        public void Dispose()
        {
            _zipArchive?.Dispose();
        }
    }
}