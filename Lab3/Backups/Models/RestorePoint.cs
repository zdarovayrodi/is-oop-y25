namespace Backups.Models
{
    using Backups.Entities;
    using Backups.Exceptions;

    public class RestorePoint : IRestorePoint
    {
        private readonly List<IBackupObject> _backupObjects;
        private List<IStorage> _storages = new List<IStorage>();

        public RestorePoint(string name, List<IBackupObject> backupObjects)
        {
            if (string.IsNullOrEmpty(name)) throw new RestorePointException("Name is null or empty");
            Name = name;
            _backupObjects = backupObjects;
            CreationDate = DateTime.Now;
        }

        public DateTime CreationDate { get; }
        public string Name { get; }
        public IReadOnlyList<IBackupObject> BackupObjects => _backupObjects.AsReadOnly();
        public IReadOnlyList<IStorage> Storages => _storages.AsReadOnly();
        public void AddStorage(IStorage storage)
        {
            _storages.Add(storage);
        }
    }
}