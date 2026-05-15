using EventBooking.Api.Infrastructure;
using EventBooking.Application;
using EventBooking.Infrastructure;
using EventBooking.Infrastructure.Authentication;
using EventBooking.Infrastructure.Persistence;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVercel", policy => 
        policy.WithOrigins("https://event-booking-pearl.vercel.app")
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()); 
});

var app = builder.Build(); // --------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Error migrating.");
    }
}

app.UseCors("AllowVercel");

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHangfireDashboard(); 

RecurringJob.AddOrUpdate<UnverifiedUserCleanupJob>(
    "Student-Accounts-Cleanup", 
    job => job.ExecuteAsync(), 
    Cron.Hourly());

app.MapControllers();

await EventBooking.Infrastructure.Persistence.DatabaseSeeder.SeedAdminUserAsync(app.Services);

app.Run();