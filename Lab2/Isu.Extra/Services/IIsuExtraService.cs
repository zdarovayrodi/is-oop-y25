namespace Isu.Extra.Services
{
    using Isu.Entities;
    using Isu.Extra.Entities;
    using Isu.Extra.Models;
    using Isu.Models;

    public interface IIsuExtraService
    {
        ExtraGroup AddGroup(GroupName name);

        ExtraStudent AddStudent(ExtraGroup group, string name);

        OgnpCourse AddOgnpCourse(Faculty faculty);

        Stream AddOgnpCourseStream(OgnpCourse course);

        void RegisterOnOgnpCourse(ExtraStudent student, OgnpCourse course, Stream stream);
    }
}
