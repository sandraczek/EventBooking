using EventBooking.Api.Infrastructure;
using EventBooking.Application;
using EventBooking.Infrastructure;
using EventBooking.Infrastructure.Authentication;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy => 
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

var app = builder.Build(); // --------------------------------------------------

app.UseCors("AllowAll");

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
    Cron.Minutely());

app.MapControllers();

await EventBooking.Infrastructure.Persistence.DatabaseSeeder.SeedAdminUserAsync(app.Services);

app.Run();