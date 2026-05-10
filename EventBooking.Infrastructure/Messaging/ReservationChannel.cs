using System.Threading.Channels;
using EventBooking.Application.Interfaces;
using EventBooking.Application.Reservations.Commands.CreateReservation;

namespace EventBooking.Infrastructure.Messaging;

public class ReservationChannel :IReservationChannel
{
    private readonly Channel<CreateReservationCommand> _channel;

    public ReservationChannel()
    {
        var options = new BoundedChannelOptions(1000)
        {
            FullMode = BoundedChannelFullMode.Wait
        };
        _channel = Channel.CreateBounded<CreateReservationCommand>(options);
    }
    
    public async Task AddToQueueAsync(CreateReservationCommand command, CancellationToken cancellationToken)
    {
        await _channel.Writer.WriteAsync(command, cancellationToken);
    }
    
    public IAsyncEnumerable<CreateReservationCommand> ReadAllAsync(CancellationToken cancellationToken)
    {
        return _channel.Reader.ReadAllAsync(cancellationToken);
    }
}