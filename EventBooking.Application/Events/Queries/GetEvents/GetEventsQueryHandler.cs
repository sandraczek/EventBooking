using EventBooking.Application.Interfaces;
using MediatR;

namespace EventBooking.Application.Events.Queries.GetEvents;

public class GetEventsQueryHandler(IEventRepository eventRepository)
    : IRequestHandler<GetEventsQuery, IEnumerable<EventDto>>
{
    public async Task<IEnumerable<EventDto>> Handle(GetEventsQuery request, CancellationToken cancellationToken)
    {
        var events = await eventRepository.GetAllAsync(cancellationToken);
        
        var dtos = events.Select(e => new EventDto(
            e.Id,
            e.Name,
            e.Description,
            e.Date,
            e.TicketPrice
        ));

        return dtos;
    }
}