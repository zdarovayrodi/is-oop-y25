namespace Isu.Extra.Entities
{
    using Isu.Extra.Exceptions;
    using Isu.Models;

    public class Lesson
    {
        public enum Week
        {
            Odd,
            Even,
        }

        public enum Day
        {
            Monday,
            Tuesday,
            Wednesday,
            Thursday,
            Friday,
            Saturday,
        }

        public Lesson(string name, Week week, Day day, GroupName group, string teacher, uint room)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new LessonException("Name cannot be null or empty");
            }

            if (group == null)
            {
                throw new LessonException("Group cannot be null");
            }

            if (string.IsNullOrWhiteSpace(teacher))
            {
                throw new LessonException("Teacher cannot be null or empty");
            }

            Name = name;
            LessonDay = day;
            Group = group;
            LessonWeek = week;
            Teacher = teacher;
            Room = room;
        }

        public Week LessonWeek { get; }
        public Day LessonDay { get; }
        public DateTime StartTime { get; }
        public string Name { get; }
        public string Teacher { get; }
        public uint Room { get; }
        public GroupName Group { get; }
    }
}