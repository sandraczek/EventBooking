namespace EventBooking.Application.Events.Queries.GetEvents;

public record EventDto(
    Guid Id, 
    string Name, 
    string Description,
    DateTime Date, 
    decimal TicketPrice);