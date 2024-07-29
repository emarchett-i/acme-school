using Acme.School.Domain.Entities;

namespace Acme.School.Tests.Utilities.Builders
{
    public class TestCourseBuilder
    {
        private Course _course = new();

        public TestCourseBuilder WithName(int id)
        {
            _course.Id = id;

            return this;
        }

        public TestCourseBuilder WithName(string name)
        {
            _course.Name = name;

            return this;
        }

        public TestCourseBuilder WithRegistrationFee(decimal registrationFee)
        {
            _course.RegistrationFee = registrationFee;

            return this;
        }

        public TestCourseBuilder WithDateRange(DateTime startDate, DateTime endDate)
        {
            _course.StartDate = startDate;
            _course.EndDate = endDate;

            return this;
        }

        public TestCourseBuilder WithEnrollment(Enrollment enrollment)
        {
            if (_course.Enrollments is null)
            {
                _course.Enrollments = new List<Enrollment>();
            }

            _course.AddEnrollment(enrollment);

            return this;
        }

        public Course Build() => _course;
    }
}
