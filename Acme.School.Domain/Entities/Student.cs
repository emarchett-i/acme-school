using Acme.School.Domain.Exceptions;

namespace Acme.School.Domain.Entities
{
    public class Student
    {
        public string Name { get; private set; }
        public int Age { get; private set; }

        public Student(string name, int age)
        {
            Name = name;
            Age = age;

            Validate();
        }

        private void Validate()
        {
            if (Age < 18)
            {
                throw new DomainException("Student should be at least 18");
            }
        }
    }
}
