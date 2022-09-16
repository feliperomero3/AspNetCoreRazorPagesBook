using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;

namespace CityBreaks.Services;

public class EmailService : IEmailSender
{
    public Task SendEmailAsync(string email, string subject, string htmlBody)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress("City Breaks", "system@localhost"));
        message.To.Add(new MailboxAddress("City Breaks member", email));
        message.Subject = subject;

        message.Body = new TextPart("html")
        {
            Text = htmlBody
        };

        using var client = new SmtpClient();

        client.Connect("localhost", 25, MailKit.Security.SecureSocketOptions.None);

        client.Send(message);
        client.Disconnect(true);

        return Task.CompletedTask;
    }
}