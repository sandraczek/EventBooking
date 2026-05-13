using MediatR;

namespace EventBooking.Application.Students.Commands.ConfirmStudentMail;

public record ConfirmStudentMailCommand(string UserId, string Token): IRequest<bool>;