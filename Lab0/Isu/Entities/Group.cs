// <copyright file="Group.cs" company="PlaceholderCompany">
namespace Isu.Entities
{
    using Isu.Models;
    using Isu.Tools;

    public class Group
    {
        private static readonly int MaxCapacity = 30;

        public Group(GroupName groupName)
        {
            GroupName = groupName;
            Students = new List<Student>();
        }

        public GroupName GroupName { get; private set; }

        public bool IsFull => Students.Count == MaxCapacity;

        public List<Student> Students { get; private set; }

        public void AddStudent(Student student)
        {
            if (IsFull)
            {
                throw new IsuException($"Group {GroupName.Name} is full");
            }

            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            if (!Students.Contains(student))
            {
                throw new IsuException($"Student {student.Name} is not in group {GroupName.Name}");
            }

            Students.Remove(student);
        }
    }
}
