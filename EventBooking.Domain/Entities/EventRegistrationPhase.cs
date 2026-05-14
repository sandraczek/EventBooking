namespace EventBooking.Domain.Entities;

public class EventRegistrationPhase
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    
    public string TargetRole { get; set; } = string.Empty; 
    
    public DateTimeOffset StartTime { get; set; } 
}