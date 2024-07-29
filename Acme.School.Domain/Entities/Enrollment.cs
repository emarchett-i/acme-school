using Acme.School.Domain.Exceptions;

namespace Acme.School.Domain.Entities
{
    public class Enrollment
    {
        public Student Student { get; private set; }
        public Course Course { get; private set; }
        public DateTime EnrollmentDate { get; private set; }
        public bool IsPaymentComplete { get; private set; }

        public Enrollment(
            Student student, 
            Course course)
        {
            Student = student;
            Course = course;
            EnrollmentDate = DateTime.Now;
            IsPaymentComplete = false;
        }

        private void Validate()
        {
            if (Student is null)
            {
                throw new DomainException("A Student is required");
            }
            if (Course is null)
            {
                throw new DomainException("A Course is required");
            }
        }

        public void CompletePayment()
        {
            if (IsPaymentComplete)
            {
                throw new DomainException("Payment already completed");
            }

            IsPaymentComplete = true;
        }
    }
}
