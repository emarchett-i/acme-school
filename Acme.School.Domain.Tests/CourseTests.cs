using Acme.School.Domain.Entities;
using Acme.School.Domain.Exceptions;
using Acme.School.Tests.Utilities.Builders;
using FluentAssertions;

namespace Acme.School.Domain.Tests
{
    public class CourseTests
    {
        #region Validate

        [Fact]
        public void Validate_WithValidParameters_ShouldWork()
        {
            //Arrange
            Course? course = null;

            //Act & Assert
            Action action = () => course = new Course(
                "name",
                0,
                DateTime.Now,
                DateTime.Now.AddMonths(1));

            action.Should().NotThrow();
            course.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null, 0, "2024-01-01", "2024-01-02", "Course name cannot be empty or whitespace")]
        [InlineData("", 0, "2024-01-01", "2024-01-02", "Course name cannot be empty or whitespace")]
        [InlineData(" ", 0, "2024-01-01", "2024-01-02", "Course name cannot be empty or whitespace")]
        [InlineData("name", -1, "2024-01-01", "2024-01-02", "Registration fee cannot be negative")]
        [InlineData("name", 1, "2024-02-01", "2024-01-02", "Start date must be earlier than end date")]
        public void Validate_WithInvalidName_ShouldThrowDomainException(
            string name,
            decimal registrationFee,
            DateTime startDate,
            DateTime endDate,
            string expectedMessage)
        {

            //Act & Assert
            Action action = () => new Course(
                name,
                registrationFee,
                startDate,
                endDate);

            action.Should()
                .Throw<DomainException>()
                .WithMessage(expectedMessage);
        }

        #endregion Validate

        #region AddEnrollment

        [Fact]
        public void AddEnrollment_WithValidEnrollment_ShouldWork()
        {
            //Arrange
            Enrollment enrollment = new TestEnrollmentBuilder()
                .WithEnrollmentDate(DateTime.Now)
                .Build();
            var course = new Course();

            //Act
            course.AddEnrollment(enrollment);

            //Assert
            course.Enrollments.Should().NotBeNull();
            course.Enrollments.Should().HaveCount(1);
        }

        [Fact]
        public void AddEnrollment_WithInvalidEnrollment_ShouldThrowDomainException()
        {
            //Arrange
            Enrollment? enrollment = null;
            var course = new Course();

            //Act
            Action action = () => course.AddEnrollment(enrollment);

            //Assert
            action.Should()
                .Throw<DomainException>()
                .WithMessage("Enrollment is required");
        }

        #endregion AddEnrollment
    }
}