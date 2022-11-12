using Backups.Models;

namespace Backups.Test
{
    using Backups.Entities;
    using Xunit;

    public class BackupTest
    {
        // [Fact]
        // public void Test1()
        // {
        //     // 1. Создаём Backup Task, использующую Split Storage
        //     // 2. Добавляем в Backup Task два Backup Object
        //     // 3. Запускаем выполнение Backup Task
        //     // 4. Удаляем из Backup Task один Backup Object
        //     // 5. Запускаем выполнение Backup Task
        //     // 6. Проверяем то, что было создано две Restore Point и три Storage
        //     IBackupTask backupTask = new BackupTask("testBackup/", new SingleStorage(), "/Users/zdarovayrodi/Documents/");
        //
        //     IBackupObject backupObject1 = new BackupObject("File.txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/");
        //     IBackupObject backupObject2 = new BackupObject("File1.txt", "/Users/zdarovayrodi/Documents/itmo-oop/Lab3/Backups/TestFiles/");
        //     backupTask.AddBackupObject(backupObject1);
        //     backupTask.AddBackupObject(backupObject2);
        //
        //     backupTask.CreateRestorePoint();
        //
        //     // backupTask.RemoveBackupObject(backupObject2);
        //
        //     // backupTask.CreateRestorePoint();
        //     Assert.Equal(1, backupTask.RestorePoints.Count);
        //
        //     // Assert.Equal(3, backupTask.Storages.Count);
        // }
    }
}