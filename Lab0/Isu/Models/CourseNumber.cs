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
        private static readonly char[] AvailableCourses = { '1', '2', '3', '4' };

        public CourseNumber(char courseNumber)
        {
            if (!AvailableCourses.Contains(courseNumber))
            {
                throw new IsuException($"Course number must be between 1 and 4");
            }

            Number = courseNumber;
        }

        public char Number { get; private set; }
    }
}