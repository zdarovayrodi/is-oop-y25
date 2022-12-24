using Backups.Entities;
using Backups.Extra.Logger;
using Backups.Extra.Merge;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskDecorator : IBackupTask
{
    private readonly IBackupTask _backupTask;
    private readonly MergeAlgorithm _mergeAlgorithm;
    private readonly Logger.Logger _logger;
    private List<IRestorePoint> _restorePoints = new List<IRestorePoint>();

    public BackupTaskDecorator(
        string backupName,
        IAlgorithm algorithm,
        string backupFullFullPath,
        IRepository repository,
        IReadOnlyList<IRestorePoint> restorePoints,
        MergeAlgorithm mergeAlgorithm,
        ILogger logger)
    {
        _backupTask = new BackupTask(backupName, algorithm, backupFullFullPath, repository);
        Algorithm = algorithm;
        RestorePoints = restorePoints;
        _mergeAlgorithm = mergeAlgorithm;
        _logger = new Logger.Logger(logger);
    }

    public IReadOnlyList<IRestorePoint> RestorePoints { get; }
    public IAlgorithm Algorithm { get; }
    public string Name { get => _backupTask.Name; }
    public string FullPath { get => _backupTask.FullPath; }
    public void AddBackupObject(IBackupObject backupObject)
    {
        _backupTask.AddBackupObject(backupObject);
        _logger.Log($"Added {backupObject.Name} to {Name}");
    }

    public void AddBackupObjects(List<IBackupObject> backupObjectList)
    {
        _backupTask.AddBackupObjects(backupObjectList);
        _logger.Log($"Added {backupObjectList.Count} objects to {Name}");
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        _backupTask.RemoveBackupObject(backupObject);
        _logger.Log($"Removed {backupObject.Name} from {Name}");
    }

    public IRestorePoint CreateRestorePoint(string restorePointName)
    {
        _restorePoints.Add(_backupTask.CreateRestorePoint(restorePointName));
        _logger.Log($"Created restore point {restorePointName} in {Name}");
        return _restorePoints.Last();
    }

    public void AddRestorePoint(IRestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
        _logger.Log($"Added restore point {restorePoint.Name} to {Name}");
    }

    public void DeleteRestorePoint(IRestorePoint restorePoint)
    {
        _backupTask.DeleteRestorePoint(restorePoint);
        _logger.Log($"Deleted restore point {restorePoint.Name} from {Name}");
    }

    public void MergeRestorePoints(IRestorePoint restorePoint1, IRestorePoint restorePoint2)
    {
        _restorePoints.Add(_mergeAlgorithm.Merge(this, restorePoint1, restorePoint2));
        _logger.Log($"Merged restore points {restorePoint1.Name} and {restorePoint2.Name} in {Name}");
    }
}