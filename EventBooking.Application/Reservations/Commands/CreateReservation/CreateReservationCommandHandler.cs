using EventBooking.Application.Interfaces;
using MediatR;
using Org.BouncyCastle.Asn1.Cms;

namespace EventBooking.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(
    IReservationChannel channel, 
    IUserRepository userRepository,
    IEventRepository eventRepository)
    : IRequestHandler<CreateReservationCommand>
{
    public async Task Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        if(request.UserRole == null) throw new InvalidOperationException($"User has no role");
        
        var userExists = await userRepository.ExistsAsync(request.UserId, cancellationToken);
        if (!userExists)
            throw new InvalidOperationException($"User with Id '{request.UserId}' does not exists.");
        
        var e = await eventRepository.GetByIdAsync(request.EventId, cancellationToken);
        if (e == null) throw new InvalidOperationException($"Event with Id '{request.EventId}' does not exists.");
        
        var phaseForUser = e.RegistrationPhases.FirstOrDefault(p => p.TargetRole == request.UserRole);
        if (phaseForUser == null)
        {
            throw new UnauthorizedAccessException($"Group '{request.UserRole}' is not permitted to this event.");
        }
        if (DateTimeOffset.UtcNow < phaseForUser.StartTime)
        {
            throw new InvalidOperationException($"Reservations for group '{request.UserRole}' start on {phaseForUser.StartTime:dd.MM.yyyy HH:mm}!");
        }

        await channel.AddToQueueAsync(request, cancellationToken);
    }
}