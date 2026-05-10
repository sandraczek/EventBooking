using EventBooking.Application.Interfaces;
using BCrypt.Net;

namespace EventBooking.Infrastructure.Authentication;

public class BcryptPasswordHasher : IPasswordHasher
{
    private const int WorkFactor = 11; 

    public string Hash(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, WorkFactor);
    }

    public bool Verify(string password, string passwordHash)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}