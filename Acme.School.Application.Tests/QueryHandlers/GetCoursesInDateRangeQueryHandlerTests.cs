using Acme.School.Application.Courses.DTOs;
using Acme.School.Application.Courses.Handlers;
using Acme.School.Application.Courses.Queries;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Interfaces;
using Acme.School.Tests.Utilities.Builders;
using FluentAssertions;
using Moq;
using Moq.AutoMock;

namespace Acme.School.Application.Tests.QueryHandlers
{
    public class GetCoursesInDateRangeQueryHandlerTests
    {
        private readonly GetCoursesInDateRangeQueryHandler _handler;
        private readonly AutoMocker _autoMocker;

        public GetCoursesInDateRangeQueryHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _handler = _autoMocker.CreateInstance<GetCoursesInDateRangeQueryHandler>();
        }

        [Fact]
        public async Task Handle_ShouldGetCourses()
        {
            //Arrange
            var query = new GetCoursesInDateRangeQuery(DateTime.Now, DateTime.Now.AddYears(1));

            Student student = new TestStudentBuilder()
                .WithId(1)
                .WithName("Frank")
                .WithAge(21)
                .Build();

            Course course = new TestCourseBuilder()
                .WithName("name")
                .WithDateRange(DateTime.Now, DateTime.Now.AddMonths(2))
                .WithRegistrationFee(5)
                .WithEnrollment(new TestEnrollmentBuilder()
                    .WithStudent(student)
                    .WithEnrollmentDate(DateTime.Now)
                    .Build())
                .Build();

            _autoMocker.GetMock<ICourseRepository>()
                .Setup(x => x.GetCoursesInDateRange(It.IsAny<DateTime>(), It.IsAny<DateTime>()))
                .ReturnsAsync([course]);

            //Act
            var result = await _handler.Handle(query, CancellationToken.None);

            //Assert
            result.Should().NotBeNullOrEmpty();
            result.Should().HaveCount(1);
            CourseDto firstCourse = result.First();
            firstCourse.Name.Should().Be(course.Name);
            firstCourse.StartDate.Should().Be(course.StartDate);
            firstCourse.EndDate.Should().Be(course.EndDate);
            firstCourse.RegistrationFee.Should().Be(course.RegistrationFee);
            firstCourse.Students.Should().NotBeNullOrEmpty();
            firstCourse.Students.Should().HaveCount(1);
            firstCourse.Students.First().Name.Should().Be(student.Name);
            firstCourse.Students.First().Age.Should().Be(student.Age);
        }
    }
}
