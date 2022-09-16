// <copyright file="GroupName.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

// Elements should be documented
#pragma warning disable SA1600

namespace Isu.Models
{
    using Isu.Tools;

    public class GroupName
    {
        private const int GroupNameLength = 5;

        private static readonly char[] AvailableLetters =
        {
            'A', 'B', 'C', 'D', 'F', 'K', 'L', 'M', 'N', 'P', 'R', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        };

        public GroupName(string name)
        {
            // incorrect name
            if (name.Length != GroupNameLength)
            {
                throw new IsuException($"Group name must be {GroupNameLength} characters long");
            }

            // correct letter
            if (!GroupName.AvailableLetters.Contains(name[0]))
            {
                throw new IsuException($"Unavailable letter - {name[0]}");
            }

            // correct groupNumber with last 2 letters
            if (!char.IsDigit(name[3]) || !char.IsDigit(name[4]))
            {
                throw new IsuException($"Unavailable group number - {name[3]}{name[4]}");
            }

            // correct courseNumber
            CourseNumber = new CourseNumber(name[2]);

            Name = name;
        }

        public string Name { get; private set; }

        public CourseNumber CourseNumber { get; private set; }
    }
}