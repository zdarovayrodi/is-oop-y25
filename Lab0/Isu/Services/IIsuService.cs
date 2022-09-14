// <copyright file="IIsuService.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Elements should be documented
#pragma warning disable SA1600

namespace Isu.Services
{
    using Isu.Entities;
    using Isu.Models;

    public interface IIsuService
    {
        Group AddGroup(GroupName name);

        Student AddStudent(Group group, string name);

        Student GetStudent(int id);

        Student? FindStudent(int id);

        List<Student> FindStudents(GroupName groupName);

        List<Student> FindStudents(CourseNumber courseNumber);

        Group? FindGroup(GroupName groupName);

        List<Group> FindGroups(CourseNumber courseNumber);

        void ChangeStudentGroup(Student student, Group newGroup);
    }
}