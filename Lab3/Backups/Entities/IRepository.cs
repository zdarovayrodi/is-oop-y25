namespace Backups.Entities
{
    public interface IRepository
    {
        Stream GetStream(string path);
    }
}