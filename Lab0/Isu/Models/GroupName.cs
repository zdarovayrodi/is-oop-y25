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
        private static readonly char[] AvailableLetters =
        {
            'A', 'B', 'C', 'D', 'F', 'K', 'L', 'M', 'N', 'P', 'R', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z',
        };

        private static readonly char[] AvailableGroupNumbers =
        {
            '1', '2', '3', '4', '5', '6', '7', '8', '9', '0',
        };

        public GroupName(string name)
        {
            // incorrect name
            if (name.Length != 5)
            {
                throw new IsuException("Group name must be 5 characters long");
            }

            // correct letter
            if (!GroupName.AvailableLetters.Contains(name[0]))
            {
                throw new IsuException($"Unavailable letter - {name[0]}");
            }


            // correct courseNumber
            CourseNumber = new CourseNumber(name[2]);

            // correct groupNumber with last 2 letters
            if (!AvailableGroupNumbers.Contains(name[3]) || !AvailableGroupNumbers.Contains(name[4]))
            {
                throw new IsuException($"Unavailable group number - {name[3]}{name[4]}");
            }

            Name = name;
        }

        public string Name { get; private set; }

        internal CourseNumber CourseNumber { get; private set; }
    }
}