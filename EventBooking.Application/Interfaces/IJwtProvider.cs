using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IJwtProvider
{
    string Generate(Student student);
}