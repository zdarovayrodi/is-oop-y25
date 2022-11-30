namespace Backups.Entities
{
    public interface IRepository
    {
        byte[] GetFile(string filePath);
        void CreateDirectory(string path);
        void Write(string path, byte[] data);
    }
}