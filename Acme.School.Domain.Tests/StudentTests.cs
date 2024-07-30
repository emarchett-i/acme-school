using Acme.School.Domain.Entities;
using Acme.School.Domain.Exceptions;
using FluentAssertions;

namespace Acme.School.Domain.Tests
{
    public class StudentTests
    {
        #region Validate

        [Fact]
        public void Validate_WithValidParameters_ShouldWork()
        {
            // Arrange
            Student? student = null;

            // Act & Assert
            Action action = () => student = new Student("Jon Snow", 18);

            action.Should().NotThrow();
            student.Should().NotBeNull();
        }

        [Theory]
        [InlineData(null, 18, "Student name cannot be empty or whitespace")]
        [InlineData("", 18, "Student name cannot be empty or whitespace")]
        [InlineData(" ", 18, "Student name cannot be empty or whitespace")]
        [InlineData("Daenerys Targaryen", 17, "Student should be at least 18")]
        public void Validate_WithInvalidParameters_ShouldThrowDomainException(
            string name,
            int age,
            string expectedMessage)
        {
            // Act & Assert
            Action action = () => new Student(name, age);

            action.Should()
                .Throw<DomainException>()
                .WithMessage(expectedMessage);
        }

        #endregion Validate
    }
}
