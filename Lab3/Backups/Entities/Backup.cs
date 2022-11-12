namespace Backups.Entities
{
    using Backups.Exceptions;
    using Backups.Models;

    public class Backup : IBackup
    {
        private readonly List<IRestorePoint> _restorePoints;

        public Backup()
        {
            _restorePoints = new List<IRestorePoint>();
        }

        public IReadOnlyList<IRestorePoint> RestorePoints => _restorePoints.AsReadOnly();

        public void AddRestorePoint(IRestorePoint restorePoint)
        {
            if (restorePoint == null) throw new BackupException("Restore point is null");
            _restorePoints.Add(restorePoint);
        }

        public void DeleteRestorePoint(IRestorePoint restorePoint)
        {
            if (restorePoint == null) throw new BackupException("Restore point is null");
            if (!_restorePoints.Contains(restorePoint)) throw new BackupException("Restore point is not found");
            _restorePoints.Remove(restorePoint);
        }
    }
}