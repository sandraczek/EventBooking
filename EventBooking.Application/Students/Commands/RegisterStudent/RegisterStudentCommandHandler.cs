using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using MediatR;

namespace EventBooking.Application.Students.Commands.RegisterStudent;

public class RegisterStudentCommandHandler(
    IStudentRepository studentRepository,
    IPasswordHasher passwordHasher)
    : IRequestHandler<RegisterStudentCommand, Guid>
{
    public async Task<Guid> Handle(RegisterStudentCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await studentRepository.IsEmailUniqueAsync(request.Email, cancellationToken);
        if (!isUnique)
        {
            throw new InvalidOperationException("Student with this email already exists.");
        }
        
        var hashedPassword = passwordHasher.Hash(request.Password);
        
        var student = Student.Create(
            request.Email,
            hashedPassword,
            request.FirstName,
            request.LastName,
            request.IndexNumber);
        
        await studentRepository.AddAsync(student, cancellationToken);
        
        return student.Id;
    }
}