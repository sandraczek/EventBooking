using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IJwtProvider
{
    Task<string> GenerateAsync(ApplicationUser user);
}