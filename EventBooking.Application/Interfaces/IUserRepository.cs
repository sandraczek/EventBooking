namespace EventBooking.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Domain.Entities.ApplicationUser>> GetAllAsync(CancellationToken cancellationToken); 
    Task<Domain.Entities.ApplicationUser?> GetAsync(Guid userId, CancellationToken cancellationToken); 
    
}