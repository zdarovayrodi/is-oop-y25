namespace Backups.Models
{
    public class Storage : IStorage
    {
        public Storage(string path)
        {
            if (!Directory.Exists(path) && !File.Exists(path)) throw new ArgumentException("Path is not valid");
            Path = path;
        }

        public string Path { get; }
    }
}