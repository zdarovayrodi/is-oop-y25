using System.Collections.ObjectModel;
using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    using Isu.Entities;
    using Isu.Models;

    public class ExtraGroup
    {
        private const int MaxStudents = 30;
        private List<Lesson> _lessons = new List<Lesson>();
        private Group _group;

        public ExtraGroup(GroupName groupName)
        {
            _group = new Group(groupName);
            Faculty = new Faculty(groupName);
        }

        public IReadOnlyCollection<Lesson> Lessons => _lessons.AsReadOnly();
        public Faculty Faculty { get; private init; }

        public GroupName GroupName => _group.GroupName;
        public ReadOnlyCollection<Student> Students => _group.Students.AsReadOnly();

        public void AddLesson(Lesson lesson)
        {
            if (lesson == null)
            {
                throw new NewGroupException("Lesson can't be null");
            }

            if (_lessons.Any(l => l.StartTime == lesson.StartTime ||
                  ((l.StartTime.AddMinutes(90) > lesson.StartTime) && (l.StartTime < lesson.StartTime)) ||
                  ((lesson.StartTime.AddMinutes(90) > l.StartTime) && (lesson.StartTime < l.StartTime))))
            {
                throw new NewGroupException("Lesson overlaps with another lesson");
            }

            _lessons.Add(lesson);
        }

        public void AddStudent(ExtraStudent student)
        {
            if (student == null)
            {
                throw new NewGroupException("Student can't be null");
            }

            if (_group.Students.Count >= MaxStudents)
            {
                throw new NewGroupException("Group is full");
            }

            _group.AddStudent(student.Student);
        }
    }
}