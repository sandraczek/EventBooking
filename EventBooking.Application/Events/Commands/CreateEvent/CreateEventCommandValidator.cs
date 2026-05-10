using FluentValidation;

namespace EventBooking.Application.Events.Commands.CreateEvent;

public class CreateEventCommandValidator : AbstractValidator<CreateEventCommand>
{
    public CreateEventCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Event Name is required.")
            .MaximumLength(150).WithMessage("Name can't be longer than 150 characters.");

        RuleFor(x => x.Description)
            .MaximumLength(2000).WithMessage("Description can't be longer than 2000 characters.");

        RuleFor(x => x.Date)
            .NotEmpty().WithMessage("Date is required.")
            .Must(BeInTheFuture).WithMessage("Date must be in the future.");

        RuleFor(x => x.MaxParticipants)
            .GreaterThan(0).WithMessage("Number of participants must be greater than 0.");

        RuleFor(x => x.TicketPrice)
            .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
    }

    private static bool BeInTheFuture(DateTime date)
    {
        return date > DateTime.UtcNow;
    }
}