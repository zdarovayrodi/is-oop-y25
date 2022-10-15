using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    using Isu.Entities;
    using Isu.Models;

    public class NewGroup : Group
    {
        private const int MaxStudents = 30;
        private List<Lesson> _lessons = new List<Lesson>();
        private List<NewStudent> _students = new List<NewStudent>();

        public NewGroup(GroupName groupName)
            : base(groupName)
        {
            Faculty = new Faculty(groupName);
        }

        public IReadOnlyList<Lesson> Lessons => _lessons;
        public Faculty Faculty { get; private init; }

        public void AddLesson(Lesson lesson)
        {
            if (lesson == null)
            {
                throw new NewGroupException("Lesson can't be null");
            }

            if (_lessons.Any(l => l.StartTime == lesson.StartTime))
            {
                throw new NewGroupException("Lesson on this time already exists");
            }

            _lessons.Add(lesson);
        }

        public void AddStudent(NewStudent student)
        {
            if (student == null)
            {
                throw new NewGroupException("Student can't be null");
            }

            if (_students.Count >= MaxStudents)
            {
                throw new NewGroupException("Group is full");
            }

            _students.Add(student);
        }
    }
}