using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IStudentRepository
{
    Task AddAsync(Student student, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid studentId, CancellationToken cancellationToken);
    Task<Student?> GetAsync(Guid studentId, CancellationToken cancellationToken);
}