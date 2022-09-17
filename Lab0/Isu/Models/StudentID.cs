namespace Isu.Models
{
    public class StudentID
    {
        private static int id = 0;
        public int NextId => id++;
    }
}