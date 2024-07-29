using Acme.School.Application.Courses.Commands;
using Acme.School.Application.Courses.Handlers;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Moq.AutoMock;

namespace Acme.School.Application.Tests.Commands
{
    public class RegisterCourseCommandHandlerTests
    {
        private readonly RegisterCourseCommandHandler _handler;
        private readonly AutoMocker _autoMocker;

        public RegisterCourseCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _handler = _autoMocker.CreateInstance<RegisterCourseCommandHandler>();
        }

        [Fact]
        public async Task Handle_ShouldRegisterNewCourse()
        {
            //Arrange
            var command = new RegisterCourseCommand(
                ".net core",
                0,
                DateTime.Now,
                DateTime.Now.AddMonths(1));

            Course? courseBeingCreated = null;
            _autoMocker.GetMock<ICourseRepository>()
                .Setup(x => x.Add(It.IsAny<Course>(), It.IsAny<CancellationToken>()))
                .Callback<Course, CancellationToken>((course, CancellationToken) =>
                {
                    courseBeingCreated = course;
                });

            //Act
            await _handler.Handle(command, CancellationToken.None);

            //Assert
            _autoMocker.GetMock<ICourseRepository>()
                .Verify(x =>
                    x.Add(It.IsAny<Course>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            courseBeingCreated.Should().NotBeNull();
            using (new AssertionScope())
            {
                courseBeingCreated.Name.Should().Be(command.Name);
                courseBeingCreated.StartDate.Should().Be(command.StartDate);
                courseBeingCreated.EndDate.Should().Be(command.EndDate);
                courseBeingCreated.RegistrationFee.Should().Be(command.RegistrationFee);
            }
        }
    }
}
