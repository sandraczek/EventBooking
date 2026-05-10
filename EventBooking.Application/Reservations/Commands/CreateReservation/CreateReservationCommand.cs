using MediatR;

namespace EventBooking.Application.Reservations.Commands.CreateReservation;

public record CreateReservationCommand(
    Guid EventId,
    Guid StudentId
    ) : IRequest<Guid>;