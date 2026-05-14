namespace EventBooking.Domain.Entities;

public class Event
{
    public Guid Id { get; private set; }
    public string Name { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTime Date { get; private set; }
    public int MaxParticipants { get; private set; }
    public decimal TicketPrice { get; private set; }
    public ICollection<EventRegistrationPhase> RegistrationPhases { get; set; } = new List<EventRegistrationPhase>();
    
    private Event() { }
    
    public Event(string name, string description, DateTime date, int maxParticipants, decimal ticketPrice)
    {
        if (string.IsNullOrWhiteSpace(name)) 
            throw new ArgumentException("Event Name is required.");
            
        if (date <= DateTime.UtcNow) 
            throw new ArgumentException("Event Date must be in the future.");
            
        if (maxParticipants <= 0) 
            throw new ArgumentException("Number of participants must be greater than zero.");
            
        if (ticketPrice < 0) 
            throw new ArgumentException("Ticket price must be positive.");

        Id = Guid.NewGuid();
        Name = name;
        Description = description;
        Date = date.ToUniversalTime();
        MaxParticipants = maxParticipants;
        TicketPrice = ticketPrice;

        var phase = new EventRegistrationPhase //TODO
        {
            EventId = Id,
            TargetRole = "Student",
            StartTime = DateTimeOffset.UtcNow
        };
        RegistrationPhases.Add(phase);
    }
}