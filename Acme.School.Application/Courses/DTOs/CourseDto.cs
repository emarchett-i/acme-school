using Acme.School.Application.Students.DTOs;

namespace Acme.School.Application.Courses.DTOs
{
    public class CourseDto
    {
        public string Name { get; set; }
        public decimal RegistrationFee { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public List<StudentDto> Students { get; set; }
    }
}
