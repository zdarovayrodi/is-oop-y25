namespace Isu.Extra.Services
{
    using Isu.Entities;
    using Isu.Extra.Entities;
    using Isu.Extra.Models;
    using Isu.Models;

    public interface IIsuExtraService
    {
        NewGroup AddGroup(GroupName name);

        NewStudent AddStudent(NewGroup group, string name);

        OgnpCourse AddOgnpCourse(Faculty faculty);

        Stream AddOgnpCourseStream(OgnpCourse course);

        void RegisterOnOgnpCourse(NewStudent student, OgnpCourse course, Stream stream);
    }
}
