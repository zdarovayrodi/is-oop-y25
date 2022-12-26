using Backups.Exceptions;
using Backups.Extra.Entities;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public class DeleteByHybridAlgorithm : IDeleteAlgorithm
{
    private IDeleteAlgorithm _dateAlgorithm;
    private IDeleteAlgorithm _countAlgorithm;
    private List<IRestorePoint> _points = new List<IRestorePoint>();

    public DeleteByHybridAlgorithm(IDeleteAlgorithm dateAlgorithm, IDeleteAlgorithm countAlgorithm, bool mustSatisfyBoth = true)
    {
        _dateAlgorithm = dateAlgorithm;
        _countAlgorithm = countAlgorithm;
        MustSatisfyBoth = mustSatisfyBoth;
    }

    public bool MustSatisfyBoth { get; set; }

    public IReadOnlyList<IRestorePoint> FindPointsToDelete(BackupTaskDecorator backupTask)
    {
        if (backupTask == null) throw new BackupException("Backup task is null");
        IReadOnlyList<IRestorePoint>? datePoints;
        IReadOnlyList<IRestorePoint>? countPoints;
        List<IRestorePoint>? pointsToDelete;
        if (MustSatisfyBoth)
        {
            datePoints = _dateAlgorithm.FindPointsToDelete(backupTask);
            countPoints = _countAlgorithm.FindPointsToDelete(backupTask);
            pointsToDelete = datePoints.Intersect(countPoints).ToList();
            if (pointsToDelete.Equals(backupTask.RestorePoints)) throw new BackupException("Can't delete all points");
            _points.AddRange(pointsToDelete);
            return _points;
        }

        datePoints = _dateAlgorithm.FindPointsToDelete(backupTask);
        countPoints = _countAlgorithm.FindPointsToDelete(backupTask);
        pointsToDelete = datePoints.Union(countPoints).ToList();
        if (pointsToDelete.Equals(backupTask.RestorePoints)) throw new BackupException("Can't delete all points");
        _points.AddRange(pointsToDelete);
        return _points;
    }

    public void DeletePoints(BackupTaskDecorator backupTask)
    {
        if (backupTask == null) throw new BackupException("Backup task is null");
        foreach (var point in _points)
        {
            backupTask.DeleteRestorePoint(point);
        }
    }
}