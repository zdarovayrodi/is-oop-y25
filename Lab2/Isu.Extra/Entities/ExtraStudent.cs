using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    using Isu.Entities;

    public class ExtraStudent : Student
    {
        private const int MaxOgnpCourses = 2;

        private List<OgnpCourse> _ognpCourses = new List<OgnpCourse>();

        public ExtraStudent(string name, int id, ExtraGroup group)
            : base(name, id)
        {
            Group = group;
            Faculty = new Faculty(group.GroupName);
        }

        public ExtraGroup Group { get; }
        public Faculty Faculty { get; }
        public IReadOnlyList<OgnpCourse> OgnpCourse => _ognpCourses.AsReadOnly();

        public void AddOgnpCourse(OgnpCourse course)
        {
            if (_ognpCourses.Count == 2)
            {
                throw new InvalidOperationException("Student already has courses");
            }

            if (Faculty == course.Faculty)
            {
                throw new NewStudentException("Student can't register for this course");
            }

            _ognpCourses.Add(course);
        }

        public void RemoveOgnpCourse(OgnpCourse course)
        {
            if (!_ognpCourses.Contains(course))
            {
                throw new InvalidOperationException("Student doesn't have this course");
            }

            _ognpCourses.Remove(course);
        }
    }
}