namespace Backups.Entities
{
    using Backups.Exceptions;

    public class Repository : IRepository
    {
        public Stream GetStream(string path)
        {
            if (!File.Exists(path))
                throw new RepositoryException("File not found");
            return new FileStream(path, FileMode.Open, FileAccess.Read);
        }
    }
}