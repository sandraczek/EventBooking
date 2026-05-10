using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistence.Repositories;

public class ReservationRepository(ApplicationDbContext dbContext) : IReservationRepository
{
    public async Task AddAsync(Reservation reservation, CancellationToken cancellationToken)
    {
        await dbContext.Reservations.AddAsync(reservation, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> GetCountByEventIdAsync(Guid eventId, CancellationToken cancellationToken)
    {
        return await dbContext.Reservations
            .AsNoTracking()
            .Where(r => r.EventId == eventId)
            .CountAsync(cancellationToken);
    }

    public async Task<bool> HasStudentAlreadyBookedAsync(Guid eventId, Guid studentId, CancellationToken cancellationToken)
    {
        return await dbContext.Reservations
            .AsNoTracking()
            .AnyAsync(r => r.EventId == eventId && r.StudentId == studentId, cancellationToken);
    }
}