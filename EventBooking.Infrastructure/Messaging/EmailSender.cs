using EventBooking.Application.Interfaces;
using EventBooking.Domain.Entities;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace EventBooking.Infrastructure.Messaging;

public class EmailSender(IConfiguration config, UserManager<ApplicationUser> userManager) : IEmailSender, IEmailer
{

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        var emailMessage = new MimeMessage();
        var settings = config.GetSection("SmtpSettings");

        emailMessage.From.Add(new MailboxAddress(settings["SenderName"], settings["SenderEmail"] ?? ""));
        emailMessage.To.Add(new MailboxAddress("", email));
        emailMessage.Subject = subject;

        var bodyBuilder = new BodyBuilder { HtmlBody = htmlMessage };
        emailMessage.Body = bodyBuilder.ToMessageBody();

        using var client = new SmtpClient();
        try
        {
            Console.WriteLine("TWOJ USERNAME ----------" + settings["Username"]);
            await client.ConnectAsync(settings["Server"] ?? "", int.Parse(settings["Port"] ?? ""), SecureSocketOptions.StartTls);
            await client.AuthenticateAsync(settings["Username"] ?? "", settings["Password"] ?? "");
            await client.SendAsync(emailMessage);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending mail: {ex.Message}");
            throw;
        }
        finally
        {
            await client.DisconnectAsync(true);
        }
    }
}