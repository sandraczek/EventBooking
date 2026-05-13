using EventBooking.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace EventBooking.Infrastructure.Authentication;

public class UnverifiedUserCleanupJob(
    UserManager<ApplicationUser> userManager,
    ILogger<UnverifiedUserCleanupJob> logger)
{

    public async Task ExecuteAsync()
    {
        var cutoffTime = DateTime.UtcNow.AddHours(-24);
        
        var deadAccounts = await userManager.Users
            .Where(u => !u.EmailConfirmed && u.CreatedAt < cutoffTime)
            .ToListAsync();

        if (deadAccounts.Count == 0) return;

        foreach (var user in deadAccounts)
        {
            var email = user.Email ?? "";
            var result = await userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                logger.LogInformation($"Deleted dead account with email: {email}");
            }
        }
    }
}