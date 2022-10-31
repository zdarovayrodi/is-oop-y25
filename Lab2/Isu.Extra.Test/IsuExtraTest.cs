using Isu.Extra.Entities;
using Isu.Extra.Models;
using Isu.Extra.Services;
using Isu.Models;
using Stream = Isu.Extra.Entities.Stream;

namespace Isu.Extra.Test
{
    using Xunit;

    public class IsuExtraTest
    {
        [Fact]
        public void CanAddOgnpCourse()
        {
            var service = new IsuExtraService();
            var groupName = new GroupName("M3206");
            var faculty = new Faculty(groupName);
            var studentCourse = new OgnpCourse(faculty);
            OgnpCourse ognpCourse = service.AddOgnpCourse(new Faculty(new GroupName("P1111")));
            Stream stream = service.AddOgnpCourseStream(ognpCourse);
            ExtraGroup group = service.AddGroup(groupName);
            ExtraStudent student = service.AddStudent(group, "Ivan Ivanov");

            service.RegisterOnOgnpCourse(student, ognpCourse, stream);

            Assert.Contains(ognpCourse, service.OgnpCourses);
        }

        [Fact]
        public void CanAddStudentToOgnpGroup()
        {
            var service = new IsuExtraService();
            var groupName = new GroupName("M3206");
            var faculty = new Faculty(groupName);
            var studentCourse = new OgnpCourse(faculty);
            OgnpCourse ognpCourse = service.AddOgnpCourse(new Faculty(new GroupName("P1111")));
            Stream stream = service.AddOgnpCourseStream(ognpCourse);
            ExtraGroup group = service.AddGroup(groupName);
            ExtraStudent student = service.AddStudent(group, "Ivan Ivanov");

            service.RegisterOnOgnpCourse(student, ognpCourse, stream);

            Assert.Contains(student, service.OgnpCourses.First().Streams.First().Students);
        }

        [Fact]
        public void CanDeleteStudentFromOgnp()
        {
            var service = new IsuExtraService();
            var groupName = new GroupName("M3206");
            var faculty = new Faculty(groupName);
            var studentCourse = new OgnpCourse(faculty);
            OgnpCourse ognpCourse = service.AddOgnpCourse(new Faculty(new GroupName("P1111")));
            Stream stream = service.AddOgnpCourseStream(ognpCourse);
            ExtraGroup group = service.AddGroup(groupName);
            ExtraStudent student = service.AddStudent(group, "Ivan Ivanov");

            service.RegisterOnOgnpCourse(student, ognpCourse, stream);

            service.UnregisterOgnpCourse(student, ognpCourse);

            Assert.DoesNotContain(student, service.OgnpCourses.First().Streams.First().Students);
        }

        [Fact]
        public void CanGetStreamFromCourse()
        {
            var service = new IsuExtraService();
            var groupName = new GroupName("M3206");
            var faculty = new Faculty(groupName);
            var studentCourse = new OgnpCourse(faculty);
            OgnpCourse ognpCourse = service.AddOgnpCourse(new Faculty(new GroupName("P1111")));
            Stream stream = service.AddOgnpCourseStream(ognpCourse);

            var streams = service.GetStreamFromCourse(ognpCourse);

            Assert.Contains(stream, streams);
        }

        [Fact]
        public void CanGetStudentListFromOgnpCourse()
        {
            var service = new IsuExtraService();
            var groupName = new GroupName("M3206");
            var faculty = new Faculty(groupName);
            var studentCourse = new OgnpCourse(faculty);
            OgnpCourse ognpCourse = service.AddOgnpCourse(new Faculty(new GroupName("P1111")));
            Stream stream = service.AddOgnpCourseStream(ognpCourse);
            ExtraGroup group = service.AddGroup(groupName);
            ExtraStudent student = service.AddStudent(group, "Ivan Ivanov");
            ExtraStudent student2 = service.AddStudent(group, "Masha Mashina");

            service.RegisterOnOgnpCourse(student, ognpCourse, stream);
            var students = service.GetStudentsFromOgnpCourse(ognpCourse, stream);

            Assert.Contains(student, students);
            Assert.DoesNotContain(student2, students);
        }

        [Fact]
        public void GetUnregisteredStudentListFromGroup()
        {
            var service = new IsuExtraService();
            var groupName = new GroupName("M3206");
            var faculty = new Faculty(groupName);
            var studentCourse = new OgnpCourse(faculty);
            OgnpCourse ognpCourse = service.AddOgnpCourse(new Faculty(new GroupName("P1111")));
            Stream stream = service.AddOgnpCourseStream(ognpCourse);
            ExtraGroup group = service.AddGroup(groupName);
            ExtraStudent student = service.AddStudent(group, "Ivan Ivanov");
            ExtraStudent studentUnregistered = service.AddStudent(group, "Semen Semenov");

            service.RegisterOnOgnpCourse(student, ognpCourse, stream);
            var unregisteredStudents = service.GetUnregisteredStudents(group);

            Assert.Contains(studentUnregistered, unregisteredStudents);
            Assert.DoesNotContain(student, unregisteredStudents);
        }
    }
}