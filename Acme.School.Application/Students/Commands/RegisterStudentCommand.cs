using MediatR;

namespace Acme.School.Application.Students.Commands
{
    public class RegisterStudentCommand : IRequest
    {
        public string Name { get; }
        public int Age { get; }

        public RegisterStudentCommand(
            string name, 
            int age)
        {
            Name = name;
            Age = age;
        }
    }
}
