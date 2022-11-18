namespace Backups.Entities;

public interface IBackupObject
{
    string Name { get; }
    string Extension { get; }
    string Path { get; }
    public void AddFile(string fileName, string fileExtension, string filePath);
    public void AddDirectory(string directoryName, string directoryPath);
}