namespace Backups.Entities
{
    public class IdFactory
    {
        private int id = 0;
        public int NextId => id++;
    }
}