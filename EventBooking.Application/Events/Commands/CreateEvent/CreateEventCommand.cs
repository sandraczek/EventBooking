using MediatR;

namespace EventBooking.Application.Events.Commands.CreateEvent;

public record CreateEventCommand(
    string Name,
    string Description,
    DateTime Date,
    int MaxParticipants,
    decimal TicketPrice) : IRequest<Guid>;