using System.Text;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace CityBreaks.Logging;

public class EmailLogger : ILogger
{
    private readonly IEmailSender _emailSender;

    public EmailLogger(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    IDisposable ILogger.BeginScope<TState>(TState state)
    {
        return null!; // This logger will not support scopes.
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == LogLevel.Error || logLevel == LogLevel.Critical;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = new StringBuilder();

        message.AppendLine($"Event ID: {eventId.Id}");
        message.AppendLine($"Message: {state?.ToString()}");
        message.AppendLine($"Level: {logLevel}");
        message.AppendLine($"Exception: {exception?.ToString()}");
        message.AppendLine();

        Task.Run(() => SendLog(message.ToString()));
    }

    private async Task SendLog(string htmlMessage)
    {
        var subject = "Error in Application";
        var to = "test@localhost";
        await _emailSender.SendEmailAsync(to, subject, htmlMessage);
    }
}
