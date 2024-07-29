using Acme.School.Application.Courses.DTOs;
using Acme.School.Application.Courses.Queries;
using Acme.School.Application.Students.DTOs;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Interfaces;
using MediatR;

namespace Acme.School.Application.Courses.Handlers
{
    public class GetCoursesInDateRangeQueryHandler : IRequestHandler<GetCoursesInDateRangeQuery, List<CourseDto>>
    {
        private readonly ICourseRepository _courseRepository;

        public GetCoursesInDateRangeQueryHandler(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public async Task<List<CourseDto>> Handle(
            GetCoursesInDateRangeQuery request, 
            CancellationToken cancellationToken)
        {
            IEnumerable<Course> courses = await _courseRepository.GetCoursesInDateRange(request.StartDate, request.EndDate);

            List<CourseDto> courseDTOs = courses.Select(course => new CourseDto
            {
                Name = course.Name,
                RegistrationFee = course.RegistrationFee,
                StartDate = course.StartDate,
                EndDate = course.EndDate,
                Students = course.Enrollments.Select(enrollment => new StudentDto
                {
                    Name = enrollment.Student.Name,
                    Age = enrollment.Student.Age
                }).ToList()
            }).ToList();

            return courseDTOs;
        }
    }
}
