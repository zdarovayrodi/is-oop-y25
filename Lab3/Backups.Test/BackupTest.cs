using Backups.Models;

namespace Backups.Test
{
    using Backups.Entities;
    using Xunit;

    public class BackupTest
    {
        [Fact]
        public void SingleStorageTest()
        {
            IBackupTask backupTask = new BackupTask(
                "testBackup/",
                new SingleStorageAlgorithm(),
                "https://github.com/is-oop-y25/zdarovayrodi/blob/lab-3/Lab3/Backups.Test/TestFiles",
                new Repository());

            IBackupObject backupObject1 = new BackupObject("File", "txt", "https://github.com/is-oop-y25/zdarovayrodi/blob/lab-3/Lab3/Backups.Test/TestFiles/File.txt");
            IBackupObject backupObject2 = new BackupObject("File1", "txt", "https://github.com/is-oop-y25/zdarovayrodi/blob/lab-3/Lab3/Backups.Test/TestFiles/File1.txt");

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
            IBackupTask backupTask = new BackupTask(
                "testBackup/",
                new SplitStorageAlgorithm(),
                "https://github.com/is-oop-y25/zdarovayrodi/blob/lab-3/Lab3/Backups.Test/TestFiles",
                new Repository());

            IBackupObject backupObject1 = new BackupObject("File", "txt", "https://github.com/is-oop-y25/zdarovayrodi/blob/lab-3/Lab3/Backups.Test/TestFiles/File.txt");
            IBackupObject backupObject2 = new BackupObject("File1", "txt", "https://github.com/is-oop-y25/zdarovayrodi/blob/lab-3/Lab3/Backups.Test/TestFiles/File1.txt");

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