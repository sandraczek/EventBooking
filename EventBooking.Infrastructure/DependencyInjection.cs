using EventBooking.Application.Interfaces;
using EventBooking.Infrastructure.Authentication;
using EventBooking.Infrastructure.Messaging;
using EventBooking.Infrastructure.Persistence;
using EventBooking.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventBooking.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPasswordHasher, BcryptPasswordHasher>();
        
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Database")));
        
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        
        services.AddSingleton<IReservationChannel, ReservationChannel>();
        services.AddHostedService<ReservationBackgroundWorker>();

        return services;
    }
}