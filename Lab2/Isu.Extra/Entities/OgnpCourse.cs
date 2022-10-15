using Isu.Extra.Exceptions;

namespace Isu.Extra.Entities
{
    using Isu.Extra.Models;

    public class OgnpCourse
    {
        private Faculty _faculty;
        private List<Stream> _streams = new List<Stream>();

        public OgnpCourse(Faculty faculty)
        {
            _faculty = faculty;
        }

        public Faculty Faculty => _faculty;
        public IReadOnlyList<Stream> Streams => _streams;

        public void AddStream(Stream stream) => _streams.Add(stream);

        public void AddStudent(NewStudent student, Stream stream)
        {
            if (student == null || stream == null)
                throw new OgnpException("Student or stream is null");

            if (!_streams.Contains(stream))
            {
                throw new ArgumentException("Stream not found");
            }

            stream.AddStudent(student);
        }

        public void RemoveStudent(NewStudent student)
        {
            if (student == null)
                throw new OgnpException("Student is null");

            var stream = _streams.FirstOrDefault(s => s.Students.Contains(student));
            if (stream == null)
            {
                throw new ArgumentException("Student not found");
            }

            stream.RemoveStudent(student);
        }
    }
}