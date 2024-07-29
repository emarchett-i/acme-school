using Acme.School.Domain.Exceptions;

namespace Acme.School.Domain.Entities
{
    public class Course
    {
        public string Name { get; private set; }
        public decimal RegistrationFee { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public List<Enrollment> Enrollments { get; private set; }

        public Course(
            string name, 
            decimal registrationFee, 
            DateTime startDate, 
            DateTime endDate)
        {
            Name = name;
            RegistrationFee = registrationFee;
            StartDate = startDate;
            EndDate = endDate;

            Validate();
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(Name))
            {
                throw new DomainException("Course name cannot be empty or whitespace");
            }

            if (RegistrationFee < 0)
            {
                throw new DomainException("Registration fee cannot be negative");
            }

            if (StartDate >= EndDate)
            {
                throw new DomainException("Start date must be earlier than end date");
            }
        }

        public void AddEnrollment(Enrollment enrollment)
        {
            if (enrollment is null)
            {
                throw new DomainException("Enrollment is required");
            }

            if (Enrollments is null)
            {
                Enrollments = new List<Enrollment>();
            }

            Enrollments.Add(enrollment);
        }
    }
}
