namespace EventBooking.Application.Reservations.Queries.GetMyReservations;

public record ReservationDto
(
    string ReservationId,
    string EventId,
    string EventTitle,
    string EventDate,
    string Status
);