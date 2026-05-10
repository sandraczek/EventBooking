using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IStudentRepository
{
    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken);
    Task AddAsync(Student student, CancellationToken cancellationToken);
    Task<bool> ExistsAsync(Guid studentId, CancellationToken cancellationToken);
}