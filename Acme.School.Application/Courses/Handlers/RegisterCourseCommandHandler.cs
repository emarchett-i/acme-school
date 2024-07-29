using Acme.School.Application.Courses.Commands;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Interfaces;
using MediatR;

namespace Acme.School.Application.Courses.Handlers
{
    public class RegisterCourseCommandHandler : IRequestHandler<RegisterCourseCommand>
    {
        private readonly ICourseRepository _courseRepository;

        public RegisterCourseCommandHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task Handle(
            RegisterCourseCommand request, 
            CancellationToken cancellationToken)
        {
            var course = new Course(
                request.Name, 
                request.RegistrationFee, 
                request.StartDate, 
                request.EndDate);

            await _courseRepository.Add(
                course, 
                cancellationToken);
        }
    }
}
