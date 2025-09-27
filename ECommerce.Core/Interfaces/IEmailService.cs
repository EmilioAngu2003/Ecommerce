namespace ECommerce.Core.Interfaces;
public interface IEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body);
}
