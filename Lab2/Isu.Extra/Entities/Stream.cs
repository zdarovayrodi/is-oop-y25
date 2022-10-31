namespace Isu.Extra.Entities
{
    using Isu.Extra.Exceptions;

    public class Stream
    {
        private const int MaxStudents = 30;
        private List<ExtraStudent> _students = new List<ExtraStudent>();
        private List<Lesson> _lessons = new List<Lesson>();
        private int _streamId;

        public Stream(int streamId)
        {
            _streamId = streamId;
        }

        public IReadOnlyList<ExtraStudent> Students => _students;
        public IReadOnlyList<Lesson> Lessons => _lessons;

        public void AddStudent(ExtraStudent student)
        {
            if (_students.Count >= MaxStudents)
            {
                throw new StreamException("Stream is full");
            }

            if (_students.Contains(student))
            {
                throw new StreamException("Student is already in stream");
            }

            _students.Add(student);
        }

        public void RemoveStudent(ExtraStudent student)
        {
            if (!_students.Contains(student))
            {
                throw new StreamException("Student is not in stream");
            }

            _students.Remove(student);
        }

        public void AddLesson(Lesson lesson)
        {
            if (_lessons.Contains(lesson))
            {
                throw new StreamException("Lesson is already in stream");
            }

            _lessons.Add(lesson);
        }
    }
}