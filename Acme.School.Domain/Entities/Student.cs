using Acme.School.Domain.Exceptions;

namespace Acme.School.Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }

        public Student() { }

        public Student(string name, int age)
        {
            Name = name;
            Age = age;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Student name cannot be empty or whitespace");
            }

            if (Age < 18)
            {
                throw new DomainException("Student should be at least 18");
            }
        }
    }
}
