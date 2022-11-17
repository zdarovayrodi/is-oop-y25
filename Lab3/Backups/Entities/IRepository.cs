namespace Backups.Entities;

public interface IRepository
{
    IReadOnlyList<string> Files { get; }
    IReadOnlyList<string> Directories { get; }
    void AddFile(string filePath);
    void AddDirectory(string directoryPath);
}