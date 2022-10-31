namespace Isu.Extra.Services
{
    using System.Security.Principal;
    using Isu.Entities;
    using Isu.Extra.Entities;
    using Isu.Extra.Exceptions;
    using Isu.Extra.Models;
    using Isu.Models;
    using Isu.Services;

    public class IsuExtraService : IIsuService
    {
        private List<ExtraGroup> _groups = new List<ExtraGroup>();
        private List<ExtraStudent> _students = new List<ExtraStudent>();
        private List<OgnpCourse> _ognpCourses = new List<OgnpCourse>();
        private IIsuService _isuService;

        private IdFactory _studentIdFactory = new IdFactory();
        private IdFactory _streamIdFactory = new IdFactory();

        public IsuExtraService()
        {
            _isuService = new IsuService();
        }

        public IReadOnlyList<OgnpCourse> OgnpCourses => _ognpCourses;

        public ExtraGroup AddGroup(GroupName name)
        {
            if (_groups.Any(g => g.GroupName == name))
            {
                throw new IsuExtraException("Group already exist");
            }

            var group = new ExtraGroup(name);
            _groups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            return _isuService.AddStudent(group, name);
        }

        public Student GetStudent(int id)
        {
            return _isuService.GetStudent(id);
        }

        public Student? FindStudent(int id)
        {
            return _isuService.FindStudent(id);
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            return _isuService.FindStudents(groupName);
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _isuService.FindStudents(courseNumber);
        }

        public Group? FindGroup(GroupName groupName)
        {
            return _isuService.FindGroup(groupName);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _isuService.FindGroups(courseNumber);
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            _isuService.ChangeStudentGroup(student, newGroup);
        }

        public ExtraStudent AddStudent(ExtraGroup group, string name)
        {
            if (group == null)
            {
                throw new IsuExtraException("Group is null");
            }

            if (group.Students.Any(s => s.Name == name))
            {
                throw new IsuExtraException("Student already exist");
            }

            var student = new ExtraStudent(name, _studentIdFactory.NextId, group);
            _students.Add(student);
            group.AddStudent(student);
            return student;
        }

        public OgnpCourse AddOgnpCourse(Faculty faculty)
        {
            if (faculty == null)
            {
                throw new IsuExtraException("Faculty is null");
            }

            if (_ognpCourses.Any(c => c.Faculty == faculty))
            {
                throw new IsuExtraException("Ognp course already exist");
            }

            var course = new OgnpCourse(faculty);
            _ognpCourses.Add(course);
            return course;
        }

        public Stream AddOgnpCourseStream(OgnpCourse course)
        {
            if (course == null)
            {
                throw new IsuExtraException("Course is null");
            }

            if (!_ognpCourses.Contains(course))
            {
                throw new IsuExtraException("Course not found");
            }

            var stream = new Stream(_streamIdFactory.NextId);
            course.AddStream(stream);
            return stream;
        }

        public void RegisterOnOgnpCourse(ExtraStudent student, OgnpCourse course, Stream stream)
        {
            if (student == null)
            {
                throw new IsuExtraException("Student is null");
            }

            if (course == null)
            {
                throw new IsuExtraException("Course is null");
            }

            if (stream == null)
            {
                throw new IsuExtraException("Stream is null");
            }

            if (!_students.Contains(student))
            {
                throw new IsuExtraException("Student not found");
            }

            if (!_ognpCourses.Contains(course))
            {
                throw new IsuExtraException("Course not found");
            }

            if (!course.Streams.Contains(stream))
            {
                throw new IsuExtraException("Stream not found");
            }

            if (student.OgnpCourse.Count == 2)
            {
                throw new IsuExtraException("Student already registered on courses");
            }

            if (student.Faculty == course.Faculty)
            {
                throw new IsuExtraException("Student can't register on the same faculty");
            }

            student.AddOgnpCourse(course);
            course.AddStudent(student, stream);
        }

        public void UnregisterOgnpCourse(ExtraStudent student, OgnpCourse course)
        {
            if (student == null)
            {
                throw new IsuExtraException("Student is null");
            }

            if (course == null)
            {
                throw new IsuExtraException("Course is null");
            }

            if (!_students.Contains(student))
            {
                throw new IsuExtraException("Student not found");
            }

            if (!_ognpCourses.Contains(course))
            {
                throw new IsuExtraException("Course not found");
            }

            if (!student.OgnpCourse.Contains(course))
            {
                throw new IsuExtraException("Student not registered on course");
            }

            student.RemoveOgnpCourse(course);
            course.RemoveStudent(student);
        }

        public IReadOnlyList<ExtraStudent> GetUnregisteredStudents(ExtraGroup group)
        {
            if (group == null)
            {
                throw new IsuExtraException("Group is null");
            }

            if (!_groups.Contains(group))
            {
                throw new IsuExtraException("Group not found");
            }

            return _students.Where(s => s.Group == group && s.OgnpCourse.Count == 0).ToList();
        }

        public IReadOnlyList<ExtraStudent> GetStudentsFromOgnpCourse(OgnpCourse ognpCourse, Stream stream)
        {
            if (ognpCourse == null)
            {
                throw new IsuExtraException("Ognp course is null");
            }

            if (stream == null)
            {
                throw new IsuExtraException("Stream is null");
            }

            if (!_ognpCourses.Contains(ognpCourse))
            {
                throw new IsuExtraException("Ognp course not found");
            }

            if (!ognpCourse.Streams.Contains(stream))
            {
                throw new IsuExtraException("Stream not found");
            }

            return stream.Students;
        }

        public IReadOnlyList<Stream> GetStreamFromCourse(OgnpCourse ognpCourse)
        {
            if (ognpCourse == null)
            {
                throw new IsuExtraException("Ognp course is null");
            }

            if (!_ognpCourses.Contains(ognpCourse))
            {
                throw new IsuExtraException("Ognp course not found");
            }

            return ognpCourse.Streams;
        }

        Group IIsuService.AddGroup(GroupName name)
        {
            return _isuService.AddGroup(name);
        }
    }
}