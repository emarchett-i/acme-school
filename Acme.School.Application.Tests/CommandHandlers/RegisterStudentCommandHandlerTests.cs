using Acme.School.Application.Students.Commands;
using Acme.School.Application.Students.Handlers;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Interfaces;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Moq.AutoMock;

namespace Acme.School.Application.Tests.CommandHandlers
{
    public class RegisterStudentCommandHandlerTests
    {
        private RegisterStudentCommandHandler _handler;
        private AutoMocker _autoMocker;

        public RegisterStudentCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _handler = _autoMocker.CreateInstance<RegisterStudentCommandHandler>();
        }

        [Fact]
        public async Task Handle_ShouldAddNewUser()
        {
            //Arrange
            var command = new RegisterStudentCommand("name", 21);
            Student? studentBeingCreated = null;
            _autoMocker.GetMock<IStudentRepository>()
                .Setup(x => x.Add(It.IsAny<Student>(), It.IsAny<CancellationToken>()))
                .Callback<Student, CancellationToken>((student, cancellationToken) => { studentBeingCreated = student; });

            //Act
            await _handler.Handle(command, CancellationToken.None);

            //Assert
            _autoMocker.GetMock<IStudentRepository>()
                .Verify(x => 
                    x.Add(It.IsAny<Student>(), It.IsAny<CancellationToken>()), 
                    Times.Once);

            studentBeingCreated.Should().NotBeNull();
            using (new AssertionScope())
            {
                studentBeingCreated.Name.Should().Be(command.Name);
                studentBeingCreated.Age.Should().Be(command.Age);
            }
        }
    }
}
