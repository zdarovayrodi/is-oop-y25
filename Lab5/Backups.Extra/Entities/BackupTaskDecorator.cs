using Backups.Entities;
using Backups.Extra.Merge;
using Backups.Models;

namespace Backups.Extra.Entities;

public class BackupTaskDecorator : IBackupTask
{
    private readonly IBackupTask _backupTask;
    private readonly MergeAlgorithm _mergeAlgorithm;
    private List<IRestorePoint> _restorePoints = new List<IRestorePoint>();

    public BackupTaskDecorator(string backupName, IAlgorithm algorithm, string backupFullFullPath, IRepository repository, IReadOnlyList<IRestorePoint> restorePoints, string name, string fullPath, MergeAlgorithm mergeAlgorithm)
    {
        _backupTask = new BackupTask(backupName, algorithm, backupFullFullPath, repository);
        Algorithm = algorithm;
        RestorePoints = restorePoints;
        Name = name;
        FullPath = fullPath;
        _mergeAlgorithm = mergeAlgorithm;
    }

    public IReadOnlyList<IRestorePoint> RestorePoints { get; }
    public IAlgorithm Algorithm { get; }
    public string Name { get; }
    public string FullPath { get; }
    public void AddBackupObject(IBackupObject backupObject)
    {
        _backupTask.AddBackupObject(backupObject);
    }

    public void AddBackupObjects(List<IBackupObject> backupObjectList)
    {
        _backupTask.AddBackupObjects(backupObjectList);
    }

    public void RemoveBackupObject(IBackupObject backupObject)
    {
        _backupTask.RemoveBackupObject(backupObject);
    }

    public IRestorePoint CreateRestorePoint(string restorePointName)
    {
        _restorePoints.Add(_backupTask.CreateRestorePoint(restorePointName));
        return _restorePoints.Last();
    }

    public void AddRestorePoint(IRestorePoint restorePoint)
    {
        _restorePoints.Add(restorePoint);
    }

    public void DeleteRestorePoint(IRestorePoint restorePoint)
    {
        _backupTask.DeleteRestorePoint(restorePoint);
    }

    public void MergeRestorePoints(IRestorePoint restorePoint1, IRestorePoint restorePoint2)
    {
        _restorePoints.Add(_mergeAlgorithm.Merge(this, restorePoint1, restorePoint2));
    }
}