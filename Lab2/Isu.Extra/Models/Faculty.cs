using Isu.Models;

namespace Isu.Extra.Models
{
    public class Faculty
    {
        private static Dictionary<char, string> _facultyNames = new Dictionary<char, string>()
        {
            { 'A', "Chemical and Biological cluster" },
            { 'B', "Applied Optics" },
            { 'C', "Design and Urbanism, Financial Cybertechnologies" },
            { 'D', "International Development and Partnership" },
            { 'K', "Infocommunication Technologies" },
            { 'L', "Laser Photonics" },
            { 'M', "Information Technology and Programming" },
            { 'N', "Information Technology Security" },
            { 'P', "Software Engineering and Computer Technology" },
            { 'R', "Control Systems and Robotics" },
            { 'T', "Food Biotechnology and Engineering" },
            { 'U', "Technological Management and Innovation" },
            { 'V', "Photonics and Optoinformatics" },
            { 'W', "Low-temperature Energy" },
            { 'X', "Extramural Studies" },
            { 'Y', "Secondary Vocational Training" },
            { 'Z', "Physics and Technology Faculty" },
        };

        public Faculty(GroupName groupName)
        {
            this.Name = _facultyNames[groupName.Name[0]];
        }

        public string Name { get; private set; }
    }
}