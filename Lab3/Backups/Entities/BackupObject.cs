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
            Extension = string.Empty;
        }

        public string Name { get; private set; }
        public string Extension { get; private set; }
        public string Path { get; private set; }

        public void AddFile(string fileName, string fileExtension, string filePath)
        {
            if (string.IsNullOrEmpty(fileName) || string.IsNullOrEmpty(filePath) || string.IsNullOrEmpty(fileExtension))
                throw new BackupObjectException("File name, extension or path is empty");
            if (!_repository.Files.Contains(filePath))
                throw new BackupObjectException("File not found");
            if (Name != string.Empty || Path != string.Empty)
                throw new BackupObjectException("Backup object already has file or directory");
            Name = fileName;
            Extension = fileExtension;
            Path = filePath;
        }

        public void AddDirectory(string directoryName, string directoryPath)
        {
            if (string.IsNullOrEmpty(directoryName) || string.IsNullOrEmpty(directoryPath))
                throw new BackupObjectException("Directory name or path is empty");
            if (!_repository.Directories.Contains(directoryPath))
                throw new BackupObjectException("Directory not found");
            if (Name != string.Empty || Path != string.Empty)
                throw new BackupObjectException("Backup object already has file or directory");
            Name = directoryName;
            Path = directoryPath;
        }
    }
}