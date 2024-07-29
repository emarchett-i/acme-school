using Acme.School.Domain.Entities;

namespace Acme.School.Domain.Interfaces
{
    public interface ICourseRepository
    {
        Task<Course> GetById(int id);

        Task<IEnumerable<Course>> GetCoursesInDateRange(DateTime startDate, DateTime endDate);

        Task Add(Course course, CancellationToken cancellationToken);
    }
}
