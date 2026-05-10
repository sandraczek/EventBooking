using EventBooking.Application.Interfaces;
using MediatR;

namespace EventBooking.Application.Reservations.Commands.CreateReservation;

public class CreateReservationCommandHandler(
    IReservationChannel channel, 
    IStudentRepository studentRepository,
    IEventRepository eventRepository)
    : IRequestHandler<CreateReservationCommand, Guid>
{
    public async Task<Guid> Handle(CreateReservationCommand request, CancellationToken cancellationToken)
    {
        var studentExists = await studentRepository.ExistsAsync(request.StudentId, cancellationToken);
        if (!studentExists)
            throw new InvalidOperationException($"Student with Id '{request.StudentId}' does not exists.");

        var eventExists = await eventRepository.Exists(request.EventId, cancellationToken);
        if (!eventExists)
            throw new InvalidOperationException($"Event with Id '{request.EventId}' does not exists.");

        await channel.AddToQueueAsync(request, cancellationToken);

        return Guid.Empty; 
    }
}