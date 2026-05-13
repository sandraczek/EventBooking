using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using EventBooking.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace EventBooking.Infrastructure.Persistence.Repositories;

public class StudentRepository(ApplicationDbContext dbContext) : IStudentRepository
{
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

    public async Task<Student?> GetAsync(Guid studentId, CancellationToken cancellationToken)
    {
        return await dbContext.Students.FindAsync(studentId, cancellationToken);
    }
}