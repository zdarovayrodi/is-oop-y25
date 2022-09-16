namespace Isu.Test
{
    using Isu.Entities;
    using Isu.Models;
    using Isu.Services;
    using Isu.Tools;
    using Xunit;

    public class IsuServiceTest
    {
        [Fact]
        public void CreateStudent_Correct()
        {
            Student student = new Student("a b c", 1111222);
            Assert.Equal("a b c", student.Name);
        }


        [Fact]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            IsuService isuService = new IsuService();
            Student student = new Student("a b c", 1);
            var group = isuService.AddGroup(new GroupName("M3201"));
            isuService.AddStudent(group, "a b c");
            try
            {
                isuService.AddStudent(group, "a b c");
            }
            catch (IsuException e)
            {
                Assert.Equal("Student already in group", e.Message);
            }
        }

        [Fact]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            IsuService isuService = new IsuService();
            Student student = new Student("a b c", 1);
            var group = isuService.AddGroup(new GroupName("M3201"));

            // add 30 random students
            for (int i = 0; i < 30; i++)
            {
                isuService.AddStudent(group, $"a b c{i}");
            }

            // add 31th student
            try
            {
                isuService.AddStudent(group, "a b c");
            }
            catch (IsuException e)
            {
                Assert.Equal($"Group {group.GroupName.Name} is full", e.Message);
            }
        }

        [Fact]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            IsuService isuService = new IsuService();

            try
            {
                isuService.AddGroup(new GroupName("M3201"));
                isuService.AddGroup(new GroupName("M3201"));
            }
            catch (IsuException e)
            {
                Assert.Equal("Group already exists", e.Message);
            }

            try
            {
                var group = isuService.AddGroup(new GroupName("M32345678201"));
            }
            catch (IsuException e)
            {
                Assert.Equal("Group name must be 5 characters long", e.Message);
            }

            try
            {
                var group = isuService.AddGroup(new GroupName("M32oa"));
            }
            catch (IsuException e)
            {
                Assert.Equal($"Unavailable group number - oa", e.Message);
            }
        }

        [Fact]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            IsuService isuService = new IsuService();
            var group1 = isuService.AddGroup(new GroupName("M3201"));
            var group2 = isuService.AddGroup(new GroupName("M3202"));
            Student student = isuService.AddStudent(group1, "a b c");
            isuService.AddStudent(group1, "d e f");  // add 2nd student to group1 to avoid exception
            isuService.ChangeStudentGroup(student, group2);

            // find student in group1
            List<Student> students = isuService.FindStudents(group1.GroupName);
            Assert.DoesNotContain(student, students);

            // find student in group2
            students = isuService.FindStudents(group2.GroupName);
            Assert.Contains(student, students);
        }
    }
}