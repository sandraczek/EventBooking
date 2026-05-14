using EventBooking.Application.Interfaces;
using EventBooking.Application.Users.Queries.GetUsers;
using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistence.Repositories;

public class UserRepository(ApplicationDbContext dbContext) : IUserRepository
{
    public async Task<IEnumerable<ApplicationUser>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .OrderBy(u => u.Email)
            .ToListAsync(cancellationToken);
    }
    public async Task<bool> ExistsAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Users
            .AsNoTracking()
            .AnyAsync(s => s.Id == userId, cancellationToken);
    }
    public async Task<ApplicationUser?> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        return await dbContext.Users.FindAsync(userId, cancellationToken);
    }
}