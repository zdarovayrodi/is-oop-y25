namespace Backups.Exceptions
{
    public class BackupObjectException : Exception
    {
        public BackupObjectException(string message)
            : base(message)
        {
        }
    }
}