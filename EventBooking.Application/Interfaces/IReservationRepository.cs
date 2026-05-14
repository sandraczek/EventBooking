using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IReservationRepository
{
    Task AddAsync(Reservation reservation, CancellationToken cancellationToken);
    
    Task<int> GetCountByEventIdAsync(Guid eventId, CancellationToken cancellationToken);
    
    Task<bool> HasUserAlreadyBookedAsync(Guid eventId, Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Reservation>> GetUserReservationsAsync(Guid userId, CancellationToken cancellationToken);
}