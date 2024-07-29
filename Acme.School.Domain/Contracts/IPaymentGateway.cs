using Acme.School.Domain.Entities;

namespace Acme.School.Domain.Contracts
{
    public interface IPaymentGateway
    {
        Task<bool> ProcessPayment(Student student, decimal registrationFee);
    }
}
