namespace Backups.Models
{
    using Backups.Entities;
    using Backups.Exceptions;

    public class RestorePoint : IRestorePoint
    {
        private readonly List<IBackupObject> _backupObjects;

        public RestorePoint(string name, List<IBackupObject> backupObjects)
        {
            if (string.IsNullOrEmpty(name)) throw new RestorePointException("Name is null or empty");
            Name = name;
            _backupObjects = backupObjects;
            CreationDate = DateTime.Now;
        }

        public DateTime CreationDate { get; }
        public string Name { get; }
        public IReadOnlyList<IBackupObject> BackupObjects => this._backupObjects.AsReadOnly();
    }
}