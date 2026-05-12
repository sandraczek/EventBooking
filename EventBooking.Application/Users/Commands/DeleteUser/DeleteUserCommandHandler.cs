using MediatR;
using Microsoft.AspNetCore.Identity;
using EventBooking.Domain.Entities;

namespace EventBooking.Application.Users.Commands.DeleteUser;

public class DeleteUserCommandHandler(UserManager<ApplicationUser> userManager)
    : IRequestHandler<DeleteUserCommand, bool>
{
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId.ToString());
        
        if (user == null)
        {
            return false;
        }

        var result = await userManager.DeleteAsync(user);

        return result.Succeeded;
    }
}