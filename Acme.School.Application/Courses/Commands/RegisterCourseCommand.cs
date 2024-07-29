using MediatR;

namespace Acme.School.Application.Courses.Commands
{
    public class RegisterCourseCommand : IRequest
    {
        public string Name { get; }
        public decimal RegistrationFee { get; }
        public DateTime StartDate { get; }
        public DateTime EndDate { get; }

        public RegisterCourseCommand(
            string name, 
            decimal registrationFee, 
            DateTime startDate, 
            DateTime endDate)
        {
            Name = name;
            RegistrationFee = registrationFee;
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
