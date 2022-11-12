namespace Backups.Entities
{
    using Backups.Exceptions;
    using Backups.Models;

    public class BackupTask : IBackupTask
    {
        private List<IBackupObject> _backupObjects = new List<IBackupObject>();
        private List<IRestorePoint> _restorePoints = new List<IRestorePoint>();

        public BackupTask(string backupName, IStorage storage)
        {
            if (string.IsNullOrEmpty(backupName)) throw new BackupException("Backup name cannot be null or empty");
            if (storage == null) throw new BackupException("Storage cannot be null");

            BackupName = backupName;
            Storage = storage;
        }

        public string BackupName { get; }

        public IReadOnlyList<IRestorePoint> RestorePoints => _restorePoints.AsReadOnly();
        public IStorage Storage { get; }

        public void AddBackupObject(IBackupObject backupObject)
        {
            if (backupObject == null) throw new BackupException("Backup object cannot be null");
            _backupObjects.Add(backupObject);
        }

        public void AddBackupObjects(List<IBackupObject> backupObjectList)
        {
            if (backupObjectList == null) throw new BackupException("Backup object list cannot be null");
            _backupObjects.AddRange(backupObjectList);
        }

        public void RemoveBackupObject(IBackupObject backupObject)
        {
            if (backupObject == null) throw new BackupException("Backup object cannot be null");
            if (!_backupObjects.Contains(backupObject)) throw new BackupException("Backup object not found");
            _backupObjects.Remove(backupObject);
        }

        public IRestorePoint CreateRestorePoint()
        {
            IRestorePoint restorePoint = new RestorePoint(BackupName, _backupObjects);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }
    }
}