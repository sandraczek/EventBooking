using MediatR;

namespace EventBooking.Application.Users.Queries.GetUsers;

public record UserDto(Guid Id, string Email);
public record GetUsersQuery : IRequest<IEnumerable<UserDto>>;