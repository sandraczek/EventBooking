using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Application.Students.Queries.Login;

public class LoginQueryHandler(
    UserManager<ApplicationUser> userManager,
    IJwtProvider jwtProvider)
    : IRequestHandler<LoginQuery, string>
{
    public async Task<string> Handle(LoginQuery request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.Email);

        if (user is null)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }
        
        var isPasswordValid = await userManager.CheckPasswordAsync(user, request.Password);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid credentials.");
        }
        
        var token = await jwtProvider.GenerateAsync(user);

        return token;
    }
}