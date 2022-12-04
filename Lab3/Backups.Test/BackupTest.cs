namespace Backups.Test
{
    using System;
    using Backups.Entities;
    using Backups.Models;
    using Xunit;

    public class BackupTest
    {
        [Fact]
        public void SingleStorageTest()
        {
            var repository = new MockRepository();
            IBackupTask backupTask = new BackupTask(
                "testBackup/",
                new SingleStorageAlgorithm(),
                "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/",
                repository);

            IBackupObject backupObject1 = new BackupObject("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt", repository);
            IBackupObject backupObject2 = new BackupObject("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt", repository);

            backupTask.AddBackupObject(backupObject1);
            backupTask.AddBackupObject(backupObject2);

            backupTask.CreateRestorePoint("TestFIRST");
            Assert.Equal(1, backupTask.RestorePoints.Count);
            Assert.Equal(2, backupTask.RestorePoints[0].Storages.Count);

            backupTask.RemoveBackupObject(backupObject2);

            backupTask.CreateRestorePoint("TESTSECOND");
            Assert.Equal(2, backupTask.RestorePoints.Count);
            Assert.Equal(1, backupTask.RestorePoints[1].Storages.Count);

            int totalStoragesCount = 0;
            foreach (var restorePoint in backupTask.RestorePoints)
            {
                totalStoragesCount += restorePoint.Storages.Count;
            }

            Assert.Equal(3, totalStoragesCount);
        }

        [Fact]
        public void SplitStorageTest()
        {
            var repository = new MockRepository();
            IBackupTask backupTask = new BackupTask(
                "testBackup/",
                new SplitStorageAlgorithm(),
                "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt",
                repository);

            IBackupObject backupObject1 = new BackupObject("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt", repository);
            IBackupObject backupObject2 = new BackupObject("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt", repository);

            backupTask.AddBackupObject(backupObject1);
            backupTask.AddBackupObject(backupObject2);

            backupTask.CreateRestorePoint("TestFIRST");
            Assert.Equal(1, backupTask.RestorePoints.Count);
            Assert.Equal(2, backupTask.RestorePoints[0].Storages.Count);

            backupTask.RemoveBackupObject(backupObject2);

            backupTask.CreateRestorePoint("TESTSECOND");
            Assert.Equal(2, backupTask.RestorePoints.Count);
            Assert.Equal(1, backupTask.RestorePoints[1].Storages.Count);

            int totalStoragesCount = 0;
            foreach (var restorePoint in backupTask.RestorePoints)
            {
                totalStoragesCount += restorePoint.Storages.Count;
            }

            Assert.Equal(3, totalStoragesCount);
        }
    }
}