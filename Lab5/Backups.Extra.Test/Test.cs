using Backups.Entities;
using Backups.Extra.Entities;
using Backups.Extra.Logger;
using Backups.Extra.Merge;
using Backups.Extra.Repository;
using Backups.Extra.Saver;
using Backups.Models;
using Xunit;

namespace Backups.Extra.Test;

public class Test
{
    [Fact]
    public void TestCanMergeRestorePoints()
    {
        var repository = new ExtraMockRepository("aotuhsnohe");
        BackupTaskDecorator backupTask = new BackupTaskDecorator(
            "test",
            new SplitStorageAlgorithm(),
            "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/",
            repository,
            new ConsoleLogger());
        IBackupObject backupObject1 = new BackupObject("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt", repository);
        IBackupObject backupObject2 = new BackupObject("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt", repository);

        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);

        backupTask.CreateRestorePoint("TestFIRST");
        Assert.Equal(1, backupTask.RestorePoints.Count);
        Assert.Equal(2, backupTask.RestorePoints[0].Storages.Count);

        backupTask.CreateRestorePoint("TestSECOND");
        Assert.Equal(2, backupTask.RestorePoints.Count);
        Assert.Equal(2, backupTask.RestorePoints[1].Storages.Count);

        MergeAlgorithm.Merge(backupTask, backupTask.RestorePoints[0], backupTask.RestorePoints[1]);
        Assert.Equal(1, backupTask.RestorePoints.Count);
    }

    [Fact]
    public void CanSaveBackupTaskState()
    {
        ExtraMockRepository rep = new ExtraMockRepository("seuonhu");

        BackupTaskDecorator backupTask = new BackupTaskDecorator(
            "test",
            new SplitStorageAlgorithm(),
            "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/",
            rep,
            new ConsoleLogger());

        IBackupObject backupObject1 = new BackupObject("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt", rep);
        IBackupObject backupObject2 = new BackupObject("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt", rep);

        backupTask.AddBackupObject(backupObject1);
        backupTask.AddBackupObject(backupObject2);

        backupTask.CreateRestorePoint("TestFIRST");
        Assert.Equal(1, backupTask.RestorePoints.Count);
        Assert.Equal(2, backupTask.RestorePoints[0].Storages.Count);

        backupTask.CreateRestorePoint("TestSECOND");
        Assert.Equal(2, backupTask.RestorePoints.Count);
        Assert.Equal(2, backupTask.RestorePoints[1].Storages.Count);

        Saver<BackupTaskDecorator> saver = new Saver<BackupTaskDecorator>("../../../../Backups.Extra.Test/backupObject.cs", backupTask);
        saver.Save();

        // BackupTaskDecorator backupTask2 = Saver<BackupTaskDecorator>.Load();
        // Assert.Equal(2, backupTask2.RestorePoints.Count);
    }
}