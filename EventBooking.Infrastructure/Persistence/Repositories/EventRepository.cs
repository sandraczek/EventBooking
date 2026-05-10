using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistence.Repositories;

public class EventRepository(ApplicationDbContext dbContext) : IEventRepository
{
    public async Task<bool> IsNameUniqueAsync(string name, CancellationToken cancellationToken)
    {
        var exists = await dbContext.Events.AnyAsync(e => e.Name == name, cancellationToken);
        return !exists; 
    }

    public async Task AddAsync(Event newEvent, CancellationToken cancellationToken)
    {
        await dbContext.Events.AddAsync(newEvent, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken); 
    }
    
    public async Task<IEnumerable<Event>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Events
            .AsNoTracking()
            .OrderBy(e => e.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<Event?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Events
            .AsNoTracking()
            .Where(e => e.Id == id)
            .FirstOrDefaultAsync(cancellationToken);
    }
    public async Task<bool> Exists(Guid id, CancellationToken cancellationToken)
    {
        return await dbContext.Events
            .AsNoTracking()
            .AnyAsync(e => e.Id == id, cancellationToken);
    }
}