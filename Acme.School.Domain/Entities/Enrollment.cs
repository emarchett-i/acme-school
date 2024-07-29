using Acme.School.Domain.Exceptions;

namespace Acme.School.Domain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public Student Student { get; set; }
        public Course Course { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public bool IsPaymentComplete { get; set; }

        public Enrollment() { }

        public Enrollment(
            Student student, 
            Course course)
        {
            Student = student;
            Course = course;
            EnrollmentDate = DateTime.Now;
            IsPaymentComplete = false;

            Validate();
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
