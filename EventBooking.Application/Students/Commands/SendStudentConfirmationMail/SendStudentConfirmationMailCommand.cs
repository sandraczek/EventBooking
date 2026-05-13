using MediatR;

namespace EventBooking.Application.Students.Commands.SendStudentConfirmationMail;

public record SendStudentConfirmationMailCommand(Guid StudentId, string BaseUrl) : IRequest<bool>;