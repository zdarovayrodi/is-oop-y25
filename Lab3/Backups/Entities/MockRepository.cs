using System.IO.Compression;

namespace Backups.Entities;

public class MockRepository : IRepository
{
    public byte[] GetFile(string filePath)
    {
        byte[] mockBytes = new byte[0];
        return mockBytes;
    }

    public void CreateDirectory(string path)
    {
    }

    public void Write(string path, byte[] data)
    {
    }

    public ZipArchive OpenArchive(string path)
    {
        return new ZipArchive(new MemoryStream(), ZipArchiveMode.Create);
    }

    public void CreateEntryFromFile(string path, string entryName)
    {
    }

    public void Dispose()
    {
    }
}