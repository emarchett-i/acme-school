using Acme.School.Application.Students.Commands;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Interfaces;
using MediatR;

namespace Acme.School.Application.Students.Handlers
{
    public class RegisterStudentCommandHandler : IRequestHandler<RegisterStudentCommand>
    {
        private readonly IStudentRepository _studentRepository;

        public RegisterStudentCommandHandler(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task Handle(
            RegisterStudentCommand request, 
            CancellationToken cancellationToken)
        {
            var student = new Student(request.Name, request.Age);
            await _studentRepository.Add(student, cancellationToken);
        }
    }
}
