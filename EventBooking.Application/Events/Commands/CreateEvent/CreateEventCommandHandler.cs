using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using MediatR;

namespace EventBooking.Application.Events.Commands.CreateEvent;

public class CreateEventCommandHandler(IEventRepository eventRepository) : IRequestHandler<CreateEventCommand, Guid>
{
    public async Task<Guid> Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var isUnique = await eventRepository.IsNameUniqueAsync(request.Name, cancellationToken);

        if (!isUnique) throw new InvalidOperationException($"Event '{request.Name}' already exists.");
        
        var newEvent = new Event(
            request.Name,
            request.Description,
            request.Date,
            request.MaxParticipants,
            request.TicketPrice
        );
        
        await eventRepository.AddAsync(newEvent, cancellationToken);
        
        return newEvent.Id;
    }
}