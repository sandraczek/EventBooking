using EventBooking.Domain.Entities;
using MediatR;

namespace EventBooking.Application.Reservations.Queries.GetMyReservations;

public record GetMyReservationsQuery(Guid UserId) : IRequest<IEnumerable<ReservationDto>>;