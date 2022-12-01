using Backups.Models;

namespace Backups.Test
{
    using Backups.Entities;
    using Xunit;

    public class BackupTest
    {
        [Fact(Skip = "Local file system doesn't work on GH")]
        public void SingleStorageTest()
        {
            IBackupTask backupTask = new BackupTask(
                "testBackup/",
                new SingleStorageAlgorithm(),
                "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/",
                new Repository());

            IBackupObject backupObject1 = new BackupObject("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt");
            IBackupObject backupObject2 = new BackupObject("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt");

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

        [Fact(Skip = "Local file system doesn't work on GH")]
        public void SplitStorageTest()
        {
            IBackupTask backupTask = new BackupTask(
                "testBackup/",
                new SplitStorageAlgorithm(),
                "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt",
                new Repository());

            IBackupObject backupObject1 = new BackupObject("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File.txt");
            IBackupObject backupObject2 = new BackupObject("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups.Test/TestFiles/File1.txt");

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