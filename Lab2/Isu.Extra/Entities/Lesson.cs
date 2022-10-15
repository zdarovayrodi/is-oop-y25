namespace Isu.Extra.Entities
{
    using Isu.Models;

    public class Lesson
    {
        public Lesson(string name, DateTime startTime, GroupName group, string teacher, uint room)
        {
            // TODO: check arguments
            StartTime = startTime;
            EndTime = startTime.AddMinutes(90);
            Name = name;
            Teacher = teacher;
            Room = room;
        }

        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private init; }
        public string Name { get; init; }
        public string Teacher { get; private set; }
        public uint Room { get; private set; }
    }
}