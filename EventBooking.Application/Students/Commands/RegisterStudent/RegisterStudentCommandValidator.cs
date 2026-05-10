using FluentValidation;

namespace EventBooking.Application.Students.Commands.RegisterStudent;

public class RegisterStudentCommandValidator : AbstractValidator<RegisterStudentCommand>
{
    public RegisterStudentCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail is required.")
            .EmailAddress().WithMessage("Not an email address.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.")
            .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Names is required.")
            .MaximumLength(100).WithMessage("Name is too long.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Last name is required.")
            .MaximumLength(100).WithMessage("Last name is too long.");

        RuleFor(x => x.IndexNumber)
            .NotEmpty().WithMessage("Index number is required.")
            .Matches("^[0-9]{6}$").WithMessage("Not an index number.");
    }
}