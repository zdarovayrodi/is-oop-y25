namespace Isu.Extra.Entities
{
    using Isu.Extra.Exceptions;
    using Isu.Models;

    public class Lesson
    {
        public Lesson(string name, DateTime startTime, GroupName group, string teacher, uint room)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new LessonException("Name cannot be null or empty");
            }

            if (startTime < DateTime.Now)
            {
                throw new LessonException("Start time cannot be in the past");
            }

            if (group == null)
            {
                throw new LessonException("Group cannot be null");
            }

            if (string.IsNullOrWhiteSpace(teacher))
            {
                throw new LessonException("Teacher cannot be null or empty");
            }

            StartTime = startTime;
            EndTime = startTime.AddMinutes(90);
            Name = name;
            Teacher = teacher;
            Room = room;
        }

        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public string Name { get; }
        public string Teacher { get; }
        public uint Room { get; }
    }
}