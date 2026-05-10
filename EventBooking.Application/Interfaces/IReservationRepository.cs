using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation, CancellationToken cancellationToken);
    
    Task<int> GetCountByEventIdAsync(Guid eventId, CancellationToken cancellationToken);
    
    Task<bool> HasStudentAlreadyBookedAsync(Guid eventId, Guid studentId, CancellationToken cancellationToken);
}