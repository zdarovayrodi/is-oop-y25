using Isu.Extra.Exceptions;
using Isu.Extra.Models;

namespace Isu.Extra.Entities
{
    using Isu.Entities;

    public class NewStudent : Student
    {
        private const int MaxOgnpCourses = 2;

        private List<OgnpCourse> _ognpCourses = new List<OgnpCourse>();

        public NewStudent(string name, int id, NewGroup group)
            : base(name, id)
        {
            Group = group;
            Faculty = new Faculty(group.GroupName);
        }

        public NewGroup Group { get; }
        public Faculty Faculty { get; }
        public IReadOnlyList<OgnpCourse> OgnpCourse => _ognpCourses.AsReadOnly();

        public void AddOgnpCourse(OgnpCourse course)
        {
            if (_ognpCourses.Count == 2)
            {
                throw new InvalidOperationException("Student already has courses");
            }

            if (!CanRegisterForOgnpCourse(course))
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

        private bool CanRegisterForOgnpCourse(OgnpCourse course) => Faculty != course.Faculty;
    }
}