using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistence.Repositories;

public class StudentRepository(ApplicationDbContext dbContext) : IStudentRepository
{
    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken)
    {
        var exists = await dbContext.Students
            .AnyAsync(s => s.Email == email, cancellationToken);
            
        return !exists;
    }
    public async Task AddAsync(Student student, CancellationToken cancellationToken)
    {
        await dbContext.Students.AddAsync(student, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    public async Task<bool> ExistsAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await dbContext.Students
            .AsNoTracking()
            .AnyAsync(s => s.Id == studentId, cancellationToken);
    }
}