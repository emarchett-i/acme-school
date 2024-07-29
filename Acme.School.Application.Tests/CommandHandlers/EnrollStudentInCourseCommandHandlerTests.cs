using Acme.School.Application.Enrollments.Commands;
using Acme.School.Application.Enrollments.Handlers;
using Acme.School.Domain.Contracts;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Interfaces;
using Acme.School.Tests.Utilities.Builders;
using FluentAssertions;
using FluentAssertions.Execution;
using Moq;
using Moq.AutoMock;

namespace Acme.School.Application.Tests.CommandHandlers
{
    public class EnrollStudentInCourseCommandHandlerTests
    {
        private readonly EnrollStudentInCourseCommandHandler _handler;
        private readonly AutoMocker _autoMocker;

        public EnrollStudentInCourseCommandHandlerTests()
        {
            _autoMocker = new AutoMocker();
            _handler = _autoMocker.CreateInstance<EnrollStudentInCourseCommandHandler>();
        }

        [Fact]
        public async Task Handle_ShouldCreateEnrollmentWithoutPayment()
        {
            //Arrange
            var command = new EnrollStudentInCourseCommand(1, 1);
            Student student = new TestStudentBuilder()
                .WithId(1)
                .WithName("Martin")
                .WithAge(21)
                .Build();
            _autoMocker.GetMock<IStudentRepository>()
                .Setup(x => x.GetById(command.StudentId))
                .ReturnsAsync(student);

            Course course = new TestCourseBuilder()
                .WithName(".NET fundamentals")
                .WithDateRange(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1))
                .WithRegistrationFee(0)
                .Build();
            _autoMocker.GetMock<ICourseRepository>()
                .Setup(x => x.GetById(command.CourseId))
                .ReturnsAsync(course);

            Enrollment? enrollmentBeingCreated = null;
            _autoMocker.GetMock<IEnrollmentRepository>()
                .Setup(x => x.Add(It.IsAny<Enrollment>(), It.IsAny<CancellationToken>()))
                .Callback<Enrollment, CancellationToken>((enrollment, cancellationToken) => { enrollmentBeingCreated = enrollment; });

            //Act
            await _handler.Handle(command, CancellationToken.None);

            //Assert
            _autoMocker.GetMock<IPaymentGateway>()
                .Verify(x => 
                    x.ProcessPayment(It.IsAny<Student>(), It.IsAny<decimal>()), 
                    Times.Never);

            _autoMocker.GetMock<IEnrollmentRepository>()
                .Verify(x => 
                    x.Add(It.IsAny<Enrollment>(), It.IsAny<CancellationToken>()), 
                    Times.Once);
            
            enrollmentBeingCreated.Should().NotBeNull();
            using (new AssertionScope())
            {
                enrollmentBeingCreated.Student.Should().BeEquivalentTo(student);
                enrollmentBeingCreated.Course.Should().BeEquivalentTo(course);
                enrollmentBeingCreated.EnrollmentDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
                enrollmentBeingCreated.IsPaymentComplete.Should().BeFalse();
            }
        }

        [Fact]
        public async Task Handle_ShouldCreateEnrollmentWithPayment()
        {
            //Arrange
            var command = new EnrollStudentInCourseCommand(1, 1);
            Student student = new TestStudentBuilder()
                .WithId(1)
                .WithName("Mauro")
                .WithAge(18)
                .Build();
            _autoMocker.GetMock<IStudentRepository>()
                .Setup(x => x.GetById(command.StudentId))
                .ReturnsAsync(student);

            Course course = new TestCourseBuilder()
                .WithName(".NET fundamentals")
                .WithDateRange(DateTime.Now.AddMonths(-1), DateTime.Now.AddMonths(1))
                .WithRegistrationFee(10)
                .Build();
            _autoMocker.GetMock<ICourseRepository>()
                .Setup(x => x.GetById(command.CourseId))
                .ReturnsAsync(course);

            Enrollment? enrollmentBeingCreated = null;
            _autoMocker.GetMock<IEnrollmentRepository>()
                .Setup(x => x.Add(It.IsAny<Enrollment>(), It.IsAny<CancellationToken>()))
                .Callback<Enrollment, CancellationToken>((enrollment, cancellationToken) => { enrollmentBeingCreated = enrollment; });

            _autoMocker.GetMock<IPaymentGateway>()
                .Setup(x => x.ProcessPayment(It.IsAny<Student>(), course.RegistrationFee))
                .ReturnsAsync(true);

            //Act
            await _handler.Handle(command, CancellationToken.None);

            //Assert
            _autoMocker.GetMock<IPaymentGateway>()
                .Verify(x =>
                    x.ProcessPayment(It.IsAny<Student>(), course.RegistrationFee),
                    Times.Once);

            _autoMocker.GetMock<IEnrollmentRepository>()
                .Verify(x =>
                    x.Add(It.IsAny<Enrollment>(), It.IsAny<CancellationToken>()),
                    Times.Once);

            enrollmentBeingCreated.Should().NotBeNull();
            using (new AssertionScope())
            {
                enrollmentBeingCreated.Student.Should().BeEquivalentTo(student);
                enrollmentBeingCreated.Course.Should().BeEquivalentTo(course);
                enrollmentBeingCreated.EnrollmentDate.Should().BeCloseTo(DateTime.Now, TimeSpan.FromSeconds(1));
                enrollmentBeingCreated.IsPaymentComplete.Should().BeTrue();
            }
        }
    }
}
