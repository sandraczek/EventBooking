using MediatR;

namespace EventBooking.Application.Students.Commands.RegisterStudent;

public record RegisterStudentCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string IndexNumber) : IRequest<Guid>;