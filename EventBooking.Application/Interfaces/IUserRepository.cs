namespace EventBooking.Application.Interfaces;

public interface IUserRepository
{
    Task<IEnumerable<Domain.Entities.ApplicationUser>> GetAllAsync(CancellationToken cancellationToken);                        
}