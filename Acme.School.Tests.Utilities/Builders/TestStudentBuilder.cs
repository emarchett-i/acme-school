using Acme.School.Domain.Entities;

namespace Acme.School.Tests.Utilities.Builders
{
    public class TestStudentBuilder
    {
        private Student _student = new();

        public TestStudentBuilder WithId(int id)
        {
            _student.Id = id;

            return this;
        }

        public TestStudentBuilder WithName(string name)
        {
            _student.Name = name;

            return this;
        }

        public TestStudentBuilder WithAge(int age)
        {
            _student.Age = age;

            return this;
        }

        public Student Build() => _student;
    }
}
