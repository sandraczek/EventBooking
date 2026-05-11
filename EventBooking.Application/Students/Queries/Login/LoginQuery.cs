using MediatR;

namespace EventBooking.Application.Students.Queries.Login;

public record LoginQuery(string Email, string Password) : IRequest<string>;