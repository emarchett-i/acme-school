using Acme.School.Domain.Entities;
using Acme.School.Domain.Exceptions;
using FluentAssertions;

namespace Acme.School.Domain.Tests
{
    public class EnrollmentTests
    {
        #region Validate

        [Fact]
        public void Validate_WithValidParameters_ShouldWork()
        {
            //Arrange
            Enrollment? enrollment = null;
            var student = new Student("John Doe", 20);
            var course = new Course(
                ".NET Fundamentals", 
                100,
                DateTime.Now, 
                DateTime.Now.AddMonths(1));

            //Act & Assert
            Action action = () => enrollment = new Enrollment(student, course);

            action.Should().NotThrow();
            enrollment.Should().NotBeNull();
        }

        [Fact]
        public void Validate_WithNullStudent_ShouldThrowDomainException()
        {
            //Arrange
            Student? student = null;
            var course = new Course(
                "Defensive coding in C#", 
                100, 
                DateTime.Now, 
                DateTime.Now.AddMonths(1));

            //Act
            Action action = () => new Enrollment(student, course);

            //Assert
            action.Should()
                .Throw<DomainException>()
                .WithMessage("A Student is required");
        }

        [Fact]
        public void Validate_WithNullCourse_ShouldThrowDomainException()
        {
            //Arrange
            var student = new Student("John Doe", 20);
            Course? course = null;

            //Act
            Action action = () => new Enrollment(student, course);

            //Assert
            action.Should()
                .Throw<DomainException>()
                .WithMessage("A Course is required");
        }

        #endregion Validate

        #region CompletePayment

        [Fact]
        public void CompletePayment_WithPaymentNotCompleted_ShouldWork()
        {
            //Arrange
            var student = new Student("John Doe", 20);
            var course = new Course(
                "Clean Code in C#", 
                100,
                DateTime.Now, 
                DateTime.Now.AddMonths(1));
            var enrollment = new Enrollment(student, course);

            //Act
            enrollment.CompletePayment();

            //Assert
            enrollment.IsPaymentComplete.Should().BeTrue();
        }

        [Fact]
        public void CompletePayment_WithPaymentAlreadyCompleted_ShouldThrowDomainException()
        {
            //Arrange
            var student = new Student("John Doe", 20);
            var course = new Course(
                "SOLID Principles in C#", 
                100,
                DateTime.Now, 
                DateTime.Now.AddMonths(1));
            var enrollment = new Enrollment(student, course);
            enrollment.CompletePayment();

            //Act
            Action action = () => enrollment.CompletePayment();

            //Assert
            action.Should()
                .Throw<DomainException>()
                .WithMessage("Payment already completed");
        }

        #endregion CompletePayment
    }
}
