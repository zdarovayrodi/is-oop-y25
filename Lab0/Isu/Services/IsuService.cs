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

        private IdFactory _student_id = new IdFactory();

        public Group AddGroup(GroupName name)
        {
            // check if its already exists
            if (_groups.Any(g => g.GroupName.Name == name.Name))
            {
                throw new IsuException("Group already exists");
            }

            var newGroup = new Group(name);
            _groups.Add(newGroup);
            return newGroup;
        }

        public Student AddStudent(Group group, string name)
        {
            var student = new Student(name, _student_id.NextId);
            _students.Add(student);
            group.AddStudent(student);
            return student;
        }

        public Student GetStudent(int id) => FindStudent(id) ?? throw new IsuException("Student not found");

        public Student? FindStudent(int id) => _students.FirstOrDefault(s => s.Id == id);

        public List<Student> FindStudents(GroupName groupName)
        {
            // find group by groupName
            Group? group = FindGroup(groupName);
            return group?.Students ?? new List<Student>();
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            List<Group> groups = FindGroups(courseNumber);
            List<Student> students = new List<Student>();
            students.AddRange(groups.SelectMany(g => g.Students));

            return students;
        }

        public Group? FindGroup(GroupName groupName) => _groups.FirstOrDefault(g => g.GroupName.Name == groupName.Name);

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            List<Group> groups = new List<Group>();
            groups.AddRange(_groups.Where(g => g.GroupName.CourseNumber == courseNumber));

            return groups;
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
            // check if group to add in exists
            if (!_groups.Exists(g => g.GroupName.Name != newGroup.GroupName.Name))
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
    }
}