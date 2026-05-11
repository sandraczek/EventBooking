using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace EventBooking.Application.Students.Commands.RegisterStudent;

public class RegisterStudentCommandHandler(
    IStudentRepository studentRepository,
    UserManager<ApplicationUser> userManager)
    : IRequestHandler<RegisterStudentCommand, Guid>
{
    public async Task<Guid> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var user = new ApplicationUser 
        { 
            UserName = request.Email, 
            Email = request.Email 
        };

        var result = await userManager.CreateAsync(user, request.Password);
        
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            throw new InvalidOperationException($"Failed to create user: {errors}");
        }
        
        var student = Student.Create(
            user.Id,
            request.FirstName,
            request.LastName,
            request.IndexNumber);
        
        await studentRepository.AddAsync(student, cancellationToken);
        
        return student.Id;
    }
}