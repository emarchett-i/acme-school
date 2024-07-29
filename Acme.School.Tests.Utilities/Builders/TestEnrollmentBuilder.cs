using Acme.School.Domain.Entities;

namespace Acme.School.Tests.Utilities.Builders
{
    public class TestEnrollmentBuilder
    {
        private Enrollment _enrollment = new();

        public TestEnrollmentBuilder WithId(int id)
        {
            _enrollment.Id = id;

            return this;
        }

        public TestEnrollmentBuilder WithStudent(Student student)
        {
            _enrollment.Student = student;

            return this;
        }

        public TestEnrollmentBuilder WithCourse(Course course)
        {
            _enrollment.Course = course;

            return this;
        }

        public TestEnrollmentBuilder WithEnrollmentDate(DateTime enrollmentDate)
        {
            _enrollment.EnrollmentDate = enrollmentDate;

            return this;
        }

        public TestEnrollmentBuilder WithEnrollmentDate(bool isCompletePayment)
        {
            _enrollment.IsPaymentComplete = isCompletePayment;

            return this;
        }

        public Enrollment Build() => _enrollment;
    }
}
