namespace Acme.School.Domain.Exceptions
{
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException(int studentId) : base($"The payment for the student <{studentId}> have failed") { }
    }
}
