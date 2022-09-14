// <copyright file="Group.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Elements should be documented
#pragma warning disable SA1600

namespace Isu.Entities
{
    using Isu.Models;
    using Isu.Tools;

    public class Group
    {
        private static int maxCapacity = 30;

        public Group(GroupName groupName)
        {
            this.GroupName = groupName;
            this.CourseNumber = this.GroupName.CourseNumber;
            this.Students = new List<Student>();
        }

        public string Name => $"{this.GroupName.Name}";

        public GroupName GroupName { get; set; }

        internal List<Student> Students { get; private set; }

        internal CourseNumber CourseNumber { get; private set; }

        internal bool IsFull => this.Students.Count == maxCapacity;

        // add student to group
        public void AddStudent(Student student)
        {
            if (this.Students.Count < 30)
            {
                this.Students.Add(student);
            }
            else
            {
                throw new IsuException($"Group {this.Name} is full");
            }
        }

        public void RemoveStudent(Student student)
        {
            // check if student is in group
            if (this.Students.Contains(student))
            {
                this.Students.Remove(student);
            }
            else
            {
                throw new IsuException($"Student {student.Name} is not in group {this.Name}");
            }
        }
    }
}
