using EventBooking.Application.Reservations.Commands.CreateReservation;

namespace EventBooking.Application.Interfaces;

public interface IReservationChannel
{
    public Task AddToQueueAsync(CreateReservationCommand command, CancellationToken cancellationToken);

    public IAsyncEnumerable<CreateReservationCommand> ReadAllAsync(CancellationToken cancellationToken);
}