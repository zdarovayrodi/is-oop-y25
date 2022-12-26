using Backups.Extra.Entities;
using Backups.Models;

namespace Backups.Extra.Algorithms;

public interface IDeleteAlgorithm
{
    IReadOnlyList<IRestorePoint> FindPointsToDelete(BackupTaskDecorator backupTask);
    void DeletePoints(BackupTaskDecorator backupTask);
}