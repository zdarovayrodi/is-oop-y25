namespace Backups.Entities
{
    using Backups.Exceptions;
    public class BackupObject : IBackupObject
    {
        private readonly IRepository _repository;
        public BackupObject(IRepository repository)
        {
            _repository = repository;
            Name = string.Empty;
            Path = string.Empty;
        }

        public void AddFile(string fileName, string filePath)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(filePath))
                throw new BackupObjectException("File name or path is empty");
            if (!_repository.Files.Contains(filePath))
                throw new BackupObjectException("File not found");
            Name = fileName;
            Path = filePath;
        }

        public void AddDirectory(string directoryName, string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrEmpty(directoryPath))
                throw new BackupObjectException("Directory name or path is empty");
            if (!_repository.Directories.Contains(directoryPath))
                throw new BackupObjectException("Directory not found");
            Name = directoryName;
            Path = directoryPath;
        }

        public string Name { get; private set; }
        public string Path { get; private set; }
    }
}