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
                "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/",
                new Repository());

            IBackupObject backupObject1 = new BackupObject("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/File.txt");
            IBackupObject backupObject2 = new BackupObject("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/File1.txt");

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

        // [Fact]
        // public void SplitStorageTest()
        // {
        //     IBackupTask backupTask = new BackupTask(
        //         "testBackup/",
        //         new SplitStorageAlgorithm(),
        //         "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/TestFiles/");
        //     IRepository repository = new Repository();
        //     repository.AddFile("/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/File.txt");
        //     repository.AddFile("/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/File1.txt");
        //     IBackupObject backupObject1 = new BackupObject(repository);
        //     IBackupObject backupObject2 = new BackupObject(repository);
        //     backupObject1.AddFile("File", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/File.txt");
        //     backupObject2.AddFile("File1", "txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/File1.txt");
        //     backupTask.AddBackupObject(backupObject1);
        //     backupTask.AddBackupObject(backupObject2);
        //
        //     backupTask.CreateRestorePoint();
        //     Assert.Equal(1, backupTask.RestorePoints.Count);
        //
        //     backupTask.RemoveBackupObject(backupObject2);
        //
        //     backupTask.CreateRestorePoint();
        //     Assert.Equal(2, backupTask.RestorePoints.Count);
        //     Assert.Equal(3, backupTask.Storages.Count);
        // }
    }
}