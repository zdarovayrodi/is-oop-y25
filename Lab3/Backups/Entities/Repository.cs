namespace Backups.Entities
{
    using Backups.Exceptions;

    public class Repository : IRepository
    {
        private List<string> _files;
        private List<string> _directories;

        public Repository()
        {
            _files = new List<string>();
            _directories = new List<string>();
        }

        public IReadOnlyList<string> Files => _files.AsReadOnly();
        public IReadOnlyList<string> Directories => _directories.AsReadOnly();

        public void AddFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new RepositoryException("Value cannot be null or whitespace");
            if (!File.Exists(filePath))
                throw new RepositoryException("File does not exist");
            if (_files.Contains(filePath))
                throw new RepositoryException("File already added");
            _files.Add(filePath);
        }

        public void AddDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new RepositoryException("Value cannot be null or whitespace");
            if (!Directory.Exists(directoryPath))
                throw new RepositoryException("Directory does not exist");
            if (_directories.Contains(directoryPath))
                throw new RepositoryException("Directory already added");
            _directories.Add(directoryPath);
        }
    }
}