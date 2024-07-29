using Acme.School.Application.Courses.DTOs;
using MediatR;

namespace Acme.School.Application.Courses.Queries
{
    public class GetCoursesInDateRangeQuery : IRequest<List<CourseDto>>
    {
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public GetCoursesInDateRangeQuery(
            DateTime startDate, 
            DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
