using MediatR;

namespace EventBooking.Application.Users.Commands.DeleteUser;

public record DeleteUserCommand(Guid UserId) : IRequest<bool>;