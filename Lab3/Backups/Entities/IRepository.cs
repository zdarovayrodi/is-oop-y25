using System.IO.Compression;

namespace Backups.Entities
{
    public interface IRepository
    {
        byte[] GetFile(string filePath);
        void CreateDirectory(string path);
        void Write(string path, byte[] data);
        ZipArchive OpenArchive(string path);
        void CreateEntryFromFile(string path, string entryName);
        void Dispose();
    }
}