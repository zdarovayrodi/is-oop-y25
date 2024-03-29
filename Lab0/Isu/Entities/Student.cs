// <copyright file="Student.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Elements should be documented
#pragma warning disable SA1600

namespace Isu.Entities
{
    using Isu.Tools;

    public class Student
    {
        public Student(string name, int id)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new IsuException("Invalid name");
            }

            Name = name;
            Id = id;
        }

        public string Name { get; private set; }

        public int Id { get; private set; }
    }
}