using Backups.Exceptions;
using Backups.Extra.Entities;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class DeleteByDateAlgorithm : IDeleteAlgorithm
{
    private readonly DateTime _date;
    private List<IRestorePoint> _pointsToDelete = new List<IRestorePoint>();

    public DeleteByDateAlgorithm(DateTime date)
    {
        _date = date;
    }

    public IReadOnlyList<IRestorePoint> FindPointsToDelete(BackupTaskDecorator backupTask)
    {
        _pointsToDelete.Clear();
        _pointsToDelete.AddRange(backupTask.RestorePoints.Where(point => point.CreationDate < _date));
        return _pointsToDelete;
    }

    public void DeletePoints(BackupTaskDecorator backupTask)
    {
        if (_pointsToDelete.Count == 0) throw new BackupException("No points to delete");
        foreach (var point in _pointsToDelete) backupTask.DeleteRestorePoint(point);
    }
}