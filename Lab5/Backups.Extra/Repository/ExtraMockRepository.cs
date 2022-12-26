using System.IO.Compression;
using Backups.Entities;

namespace Backups.Extra.Repository;

public class ExtraMockRepository : IRepository
{
    public ExtraMockRepository(string path)
    {
        Path = path;
    }

    public string Path { get; }
    public byte[] GetFile(string filePath)
    {
        var bytes = new byte[16];
        return bytes;
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