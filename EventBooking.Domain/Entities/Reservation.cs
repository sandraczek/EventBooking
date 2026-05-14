using EventBooking.Domain.Enums;

namespace EventBooking.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }
    public Guid EventId { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ReservationDate { get; private set; }
    public ReservationStatus Status { get; private set; }

    public Event Event { get; private set; } = null!;
    
    private Reservation() { }

    public Reservation(Guid eventId, Guid userId, ReservationStatus status)
    {
        if (eventId == Guid.Empty)
            throw new ArgumentException("Event Id cannot be empty.");

        if (userId == Guid.Empty)
            throw new ArgumentException("Student Id cannot be empty.");

        Id = Guid.NewGuid();
        EventId = eventId;
        UserId = userId;
        ReservationDate = DateTime.UtcNow;
        Status = status;
    }
}