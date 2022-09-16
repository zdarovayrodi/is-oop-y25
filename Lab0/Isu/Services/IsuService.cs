// <copyright file="IsuService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
namespace Isu.Services
{
    using System.Linq;
    using Isu.Entities;
    using Isu.Models;
    using Isu.Tools;

    public class IsuService : IIsuService
    {
        private List<Group> _groups = new List<Group>();
        private List<Student> _students = new List<Student>();

        public Group AddGroup(GroupName name)
        {
            // check if its already exists
            if (_groups.Any(g => g.GroupName.Name == name.Name))
            {
                throw new IsuException("Group already exists");
            }

            _groups.Add(new Group(name));
            return _groups.Last();
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, GetNextId());
            _students.Add(student);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            Student? student = FindStudent(id);
            return student ?? throw new IsuException("Student not found");
        }

        public Student? FindStudent(int id)
        {
            return _students.FirstOrDefault(s => s.Id == id);
        }

        public List<Student> FindStudents(GroupName groupName)
        {
            // find group by groupName
            Group group = _groups.FirstOrDefault(g => g.GroupName.Name == groupName.Name) ?? throw new IsuException("Group not found");

            return group.Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Group> groups = FindGroups(courseNumber);

            if (groups.Count == 0)
            {
                throw new IsuException($"Group with courseNumber {courseNumber} not found");
            }

            List<Student> students = new List<Student>();
            foreach (Group group in groups.Where(group => group.CourseNumber == courseNumber))
            {
                students.AddRange(group.Students);
            }

            return students;
        }

        public Group? FindGroup(GroupName groupName)
        {
            return _groups.FirstOrDefault(g => g.GroupName.Name == groupName.Name);
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            if (_groups.Count == 0)
            {
                throw new IsuException("No groups are created");
            }

            List<Group> groups = _groups.Where(group => group.CourseNumber == courseNumber).ToList();

            if (groups.Count == 0)
            {
                throw new IsuException("Groups not found");
            }

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            // check if group to add in exists
            if (!_groups.Exists( g => g.GroupName.Name != newGroup.GroupName.Name))
            {
                throw new IsuException($"Group {newGroup.GroupName.Name} not found");
            }

            // find group where student is
            Group group = _groups.FirstOrDefault(g => g.Students.Any(s => s.Id == student.Id))
                          ?? throw new IsuException($"Student {student.Name} {student.Id} not found");

            // check newGroup is not full
            if (newGroup.IsFull)
            {
                throw new IsuException($"Group {newGroup.GroupName.Name} is full");
            }

            // remove student from old group (all checks are inside RemoveStudent func)
            group.RemoveStudent(student);

            // add student to new group
            newGroup.AddStudent(student);
        }

        private int GetNextId()
        {
            return _students.Count == 0 ? 1 : _students.Max(g => g.Id) + 1;
        }
    }
}