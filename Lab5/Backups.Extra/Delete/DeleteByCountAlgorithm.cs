using Backups.Exceptions;
using Backups.Extra.Entities;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class DeleteByCountAlgorithm : IDeleteAlgorithm
{
    private List<IRestorePoint> _restorePoints = new List<IRestorePoint>();
    private int _amount;

    public DeleteByCountAlgorithm(int amount)
    {
        if (amount < 0) throw new BackupException("Amount of restore points can't be less than 0");
        _amount = amount;
    }

    public IReadOnlyList<IRestorePoint> FindPointsToDelete(BackupTaskDecorator backupTask)
    {
        _restorePoints.Clear();
        _restorePoints.AddRange(backupTask.RestorePoints.Take(backupTask.RestorePoints.Count - _amount));
        return _restorePoints;
    }

    public void DeletePoints(BackupTaskDecorator backupTask)
    {
        if (_restorePoints.Count == 0) throw new BackupException("No points to delete");
        foreach (var point in _restorePoints) backupTask.DeleteRestorePoint(point);
    }
}