// <copyright file="CourseNumber.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Elements should be documented
#pragma warning disable SA1600

namespace Isu.Models
{
    using Isu.Tools;

    public class CourseNumber
    {
        private static char[] availableCourses = { '1', '2', '3', '4' };

        public CourseNumber(char courseNumber)
        {
            if (availableCourses.Contains(courseNumber))
            {
                this.Number = courseNumber;
            }
            else
            {
                throw new IsuException($"Course number must be between 1 and 4");
            }
        }

        public char Number { get; private set; }
    }
}