using MediatR;

namespace Acme.School.Application.Enrollments.Commands
{
    public class EnrollStudentInCourseCommand : IRequest
    {
        public int StudentId { get; }
        public int CourseId { get; }

        public EnrollStudentInCourseCommand(
            int studentId, 
            int courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }
    }
}
