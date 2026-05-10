using MediatR;

namespace EventBooking.Application.Events.Queries.GetEvents;

public record GetEventsQuery() : IRequest<IEnumerable<EventDto>>;