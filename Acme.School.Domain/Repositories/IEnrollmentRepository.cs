using Acme.School.Domain.Entities;

namespace Acme.School.Domain.Interfaces
{
    public interface IEnrollmentRepository
    {
        Task Add(Enrollment enrollment, CancellationToken cancellationToken);
    }
}
