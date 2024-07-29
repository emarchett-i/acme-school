namespace Acme.School.Domain.Exceptions
{
    public class PaymentFailedException : Exception
    {
        public PaymentFailedException(int studentId, decimal fee) : base($"The payment of fee <{fee}> for the student <{studentId}> have failed") { }
    }
}
