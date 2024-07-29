using Acme.School.Domain.Entities;

namespace Acme.School.Domain.Interfaces
{
    public interface IStudentRepository
    {
        Task<Student> GetById(int id);

        Task Add(Student student, CancellationToken cancellationToken);
    }
}
