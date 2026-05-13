using System.Text;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;

namespace EventBooking.Application.Students.Commands.ConfirmStudentMail;

public class ConfirmStudentMailCommandHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<ConfirmStudentMailCommand, bool>
{
    public async Task<bool> Handle(ConfirmStudentMailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(request.UserId);
        if (user == null) return false;
        
        var decodedTokenBytes = WebEncoders.Base64UrlDecode(request.Token);
        var decodedToken = Encoding.UTF8.GetString(decodedTokenBytes);

        var result = await userManager.ConfirmEmailAsync(user, decodedToken);

        return result.Succeeded;
    }
}