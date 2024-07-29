using Acme.School.Application.Enrollments.Commands;
using Acme.School.Domain.Contracts;
using Acme.School.Domain.Entities;
using Acme.School.Domain.Exceptions;
using Acme.School.Domain.Interfaces;
using MediatR;

namespace Acme.School.Application.Enrollments.Handlers
{
    public class EnrollStudentInCourseCommandHandler : IRequestHandler<EnrollStudentInCourseCommand>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IPaymentGateway _paymentGateway;

        public EnrollStudentInCourseCommandHandler(
            IStudentRepository studentRepository, 
            ICourseRepository courseRepository, 
            IEnrollmentRepository enrollmentRepository, 
            IPaymentGateway paymentGateway)
        {
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _enrollmentRepository = enrollmentRepository;
            _paymentGateway = paymentGateway;
        }

        public async Task Handle(
            EnrollStudentInCourseCommand request, 
            CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetById(request.StudentId);
            var course = await _courseRepository.GetById(request.CourseId);

            if (student is null || course is null)
            {
                throw new ArgumentException("Student or Course not found");
            }

            var enrollment = new Enrollment(student, course);

            await PerformPaymentIfApplicable(
                student, 
                course, 
                enrollment);

            await _enrollmentRepository.Add(enrollment, cancellationToken);
        }

        private async Task PerformPaymentIfApplicable(
            Student student, 
            Course course, 
            Enrollment enrollment)
        {
            if (course.RegistrationFee <= 0)
            {
                return;
            }

            bool paymentSuccessful = await _paymentGateway.ProcessPayment(student, course.RegistrationFee);
            if (!paymentSuccessful)
            {
                throw new PaymentFailedException(student.Id, course.RegistrationFee);
            }

            enrollment.CompletePayment();
        }
    }
}
