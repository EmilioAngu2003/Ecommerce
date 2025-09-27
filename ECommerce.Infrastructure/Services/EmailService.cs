using ECommerce.Core.Interfaces;

namespace ECommerce.Infrastructure.Services;

public class EmailService : IEmailService
{
    public Task SendEmailAsync(string toEmail, string subject, string body)
    {
        Console.WriteLine("--------------------- Sending Email ---------------------");
        Console.WriteLine($"To: {toEmail}");
        Console.WriteLine($"Subject: {subject}");
        Console.WriteLine($"Body: {body}");
        Console.WriteLine("---------------------------------------------------------");

        return Task.CompletedTask;
    }
}
