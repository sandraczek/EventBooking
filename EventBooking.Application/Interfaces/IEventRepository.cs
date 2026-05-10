using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IEventRepository
{
    Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken);
    Task AddAsync(Event newEvent, CancellationToken cancellationToken);
    Task<IEnumerable<Domain.Entities.Event>> GetAllAsync(CancellationToken cancellationToken);
    Task<EventBooking.Domain.Entities.Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> Exists(Guid id, CancellationToken cancellationToken);
}