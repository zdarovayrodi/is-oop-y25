namespace Isu.Models
{
    public static class StudentID
    {
        private static List<int> _studentIDs = new List<int>();

        public static int NextID()
        {
            int nextId = _studentIDs.Count + 1;
            _studentIDs.Add(nextId);
            return nextId;
        }
    }
}