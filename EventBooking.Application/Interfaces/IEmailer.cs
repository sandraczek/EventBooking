using EventBooking.Domain.Entities;

namespace EventBooking.Application.Interfaces;

public interface IEmailer
{
    Task SendEmailAsync(string email, string subject, string htmlMessage);
}