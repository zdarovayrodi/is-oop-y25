namespace Backups.Entities
{
    using Backups.Models;

    public interface IBackup
    {
        IReadOnlyList<IRestorePoint> RestorePoints { get; }
        void AddRestorePoint(IRestorePoint restorePoint);
        void DeleteRestorePoint(IRestorePoint restorePoint);
    }
}