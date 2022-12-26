using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Models;

namespace Backups.Extra.Merge;

public class MergeAlgorithm
{
    public static IRestorePoint Merge(BackupTaskDecorator backupTaskDecorator, IRestorePoint oldRestorePoint, IRestorePoint newRestorePoint)
    {
        if (backupTaskDecorator.Algorithm == new SingleStorageAlgorithm())
        {
            backupTaskDecorator.DeleteRestorePoint(oldRestorePoint);
            backupTaskDecorator.AddRestorePoint(newRestorePoint);
            return newRestorePoint;
        }

        var oldFiles = oldRestorePoint.BackupObjects;
        var newFiles = newRestorePoint.BackupObjects;
        var mergedFiles = new List<IBackupObject>();
        foreach (var oldFile in oldFiles)
        {
            if (newFiles.All(newFile => newFile.FullPath != oldFile.FullPath)) mergedFiles.Add(oldFile);
            else if (newFiles.Any(newFile => newFile.FullPath == oldFile.FullPath)) mergedFiles.Add(newFiles.First(newFile => newFile.FullPath == oldFile.FullPath));
        }

        backupTaskDecorator.DeleteRestorePoint(oldRestorePoint);
        backupTaskDecorator.DeleteRestorePoint(newRestorePoint);
        return backupTaskDecorator.AddRestorePoint(new RestorePoint($"{newRestorePoint.Name}_merged", mergedFiles));
    }
}