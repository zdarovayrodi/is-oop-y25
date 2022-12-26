namespace Backups.Entities
{
    using System.IO.Compression;
    using Backups.Exceptions;
    using Backups.Models;

    public class BackupTask : IBackupTask
    {
        private List<IBackupObject> _backupObjects = new List<IBackupObject>();
        private List<IRestorePoint> _restorePoints = new List<IRestorePoint>();
        private IdFactory _idFactory = new IdFactory();
        private IRepository _repository;

        public BackupTask(string backupName, IAlgorithm algorithm, string backupFullFullPath, IRepository repository)
        {
            if (string.IsNullOrEmpty(backupName)) throw new BackupException("Backup name cannot be null or empty");

            Algorithm = algorithm ?? throw new BackupException("Storage cannot be null");
            Name = backupName;
            FullPath = backupFullFullPath;
            _repository = repository;
        }

        public string FullPath { get; }
        public string Name { get; }

        public IReadOnlyList<IRestorePoint> RestorePoints => _restorePoints.AsReadOnly();
        public IAlgorithm Algorithm { get; }

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

        public IRestorePoint CreateRestorePoint(string restorePointName)
        {
            int id = _idFactory.NextId;
            _repository.CreateDirectory(Path.Combine(FullPath, id.ToString()));
            if (string.IsNullOrEmpty(restorePointName))
                restorePointName = $"RestorePoint_{id}";
            _repository.CreateDirectory(Path.Combine(FullPath, restorePointName));
            IRestorePoint restorePoint = new RestorePoint(restorePointName + id, _backupObjects);
            Algorithm.Compress(this, restorePoint, id, _repository);

            // byte[] compressedData = Algorithm.Compress(this, restorePoint, id);
            // _repository.Write($"{FullPath}{restorePointName} {id}.zip", compressedData);
            _restorePoints.Add(restorePoint);
            return restorePoint;
        }

        public void DeleteRestorePoint(IRestorePoint restorePoint)
        {
            if (restorePoint == null) throw new BackupException("Restore point cannot be null");
            if (!_restorePoints.Contains(restorePoint)) throw new BackupException("Restore point not found");
            _restorePoints.Remove(restorePoint);
        }
    }
}