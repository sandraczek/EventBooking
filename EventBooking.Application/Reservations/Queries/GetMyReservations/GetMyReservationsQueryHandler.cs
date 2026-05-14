using System.Globalization;
using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using MediatR;

namespace EventBooking.Application.Reservations.Queries.GetMyReservations;

public class GetMyReservationsQueryHandler(IReservationRepository reservationRepository) : IRequestHandler<GetMyReservationsQuery, IEnumerable<ReservationDto>>
{
    public async Task<IEnumerable<ReservationDto>> Handle(GetMyReservationsQuery request, CancellationToken cancellationToken)
    {
        var reservations = await reservationRepository.GetUserReservationsAsync(request.UserId, cancellationToken);

        var dtos = reservations.Select(r => new ReservationDto(
            r.Id.ToString(),
            r.EventId.ToString(),
            r.Event.Name,
            r.Event.Date.ToString("o"),
            r.Status.ToString()
            ));

        return dtos;
    }
}