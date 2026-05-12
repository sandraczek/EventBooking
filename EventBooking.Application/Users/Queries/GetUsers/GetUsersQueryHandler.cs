using EventBooking.Application.Interfaces;
using MediatR;

namespace EventBooking.Application.Users.Queries.GetUsers;

public class GetUsersQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUsersQuery, IEnumerable<UserDto>>
{
    public async Task<IEnumerable<UserDto>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(cancellationToken);
        
        var dtos = users.Select(u => new UserDto(u.Id, u.Email ?? ""));

        return dtos;
    }
}