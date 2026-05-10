using EventBooking.Domain.Enums;

namespace EventBooking.Domain.Entities;

public class Reservation
{
    public Guid Id { get; private set; }
    public Guid EventId { get; private set; }
    public Guid StudentId { get; private set; }
    public DateTime ReservationDate { get; private set; }
    public ReservationStatus Status { get; private set; }
    
    private Reservation() { }

    public Reservation(Guid eventId, Guid studentId, ReservationStatus status)
    {
        if (eventId == Guid.Empty)
            throw new ArgumentException("Event Id cannot be empty.");

        if (studentId == Guid.Empty)
            throw new ArgumentException("Student Id cannot be empty.");

        Id = Guid.NewGuid();
        EventId = eventId;
        StudentId = studentId;
        ReservationDate = DateTime.UtcNow;
        Status = status;
    }
}