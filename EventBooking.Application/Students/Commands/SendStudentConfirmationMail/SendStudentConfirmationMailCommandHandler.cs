using System.Text;
using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;

namespace EventBooking.Application.Students.Commands.SendStudentConfirmationMail;

public class SendStudentConfirmationMailCommandHandler(
    IUserRepository userRepository,
    IStudentRepository studentRepository,
    IEmailer emailer,
    IConfiguration config,
    UserManager<ApplicationUser> userManager
    ) : IRequestHandler<SendStudentConfirmationMailCommand, bool>
{
    public async Task<bool> Handle(SendStudentConfirmationMailCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetAsync(request.StudentId, cancellationToken);
        if (user == null || user.EmailConfirmed) return false;
        
        var student = await studentRepository.GetAsync(request.StudentId, cancellationToken);
        if (student == null) return false;
        
        var token = await userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));

        var baseUrl = config["FrontendSettings:BaseUrl"];
        if (string.IsNullOrEmpty(baseUrl)) throw new Exception("Config error: no frontend url.");
        
        var endpointPath = $"{baseUrl}/api/students/confirm-email";
        var queryParams = new Dictionary<string, string?>
        {
            { "userId", user.Id.ToString() },
            { "token", encodedToken }
        };
        
        var confirmationLink = QueryHelpers.AddQueryString(endpointPath, queryParams);
        await emailer.SendEmailAsync(
            student.UniversityEmail, // UNI email for student
            "Confirm your e-mail", 
            $"Click this link to confirm your email: <a href='{confirmationLink}'>CONFIRM</a>"
            );

        return true;
    }
}