namespace Backups.Entities
{
    using Backups.Exceptions;
    public class BackupObject : IBackupObject
    {
        private IRepository _repository;
        public BackupObject(string name, string extension, string fullPath, IRepository repository)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(fullPath))
                throw new BackupObjectException("Null or empty error");
            Name = name;
            FullPath = fullPath;
            Extension = extension;
            _repository = repository;
            FileBytes = _repository.GetFile(fullPath);
        }

        public string Name { get; private set; }
        public string Extension { get; private set; }
        public string FullPath { get; private set; }
        public byte[] FileBytes { get; }
    }
}