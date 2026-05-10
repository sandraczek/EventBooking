using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using EventBooking.Domain.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EventBooking.Infrastructure.Messaging;

public class ReservationBackgroundWorker(
    IReservationChannel channel,
    IServiceScopeFactory scopeFactory,
    ILogger<ReservationBackgroundWorker> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        logger.LogInformation("Reservation Worker executed.");
        
        await foreach (var command in channel.ReadAllAsync(stoppingToken))
        {
            try
            {
                using var scope = scopeFactory.CreateScope();
                var eventRepository = scope.ServiceProvider.GetRequiredService<IEventRepository>();
                var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRepository>();

                var e = await eventRepository.GetByIdAsync(command.EventId, stoppingToken);
                if (e == null) continue;

                var currentBookings = await reservationRepository.GetCountByEventIdAsync(command.EventId, stoppingToken);

                var status = currentBookings < e.MaxParticipants 
                    ? ReservationStatus.Confirmed 
                    : ReservationStatus.ReserveList;

                var reservation = new Reservation(command.EventId, command.StudentId, status);
                await reservationRepository.AddAsync(reservation, stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error handling {StudentId} reservation request.", command.StudentId);
            }
        }
    }
}