using Microsoft.AspNetCore.Identity.UI.Services;

namespace CityBreaks.Logging;

public class EmailLoggerProvider : ILoggerProvider
{
    private readonly IEmailSender _emailService;

    public EmailLoggerProvider(IEmailSender emailSender)
    {
        _emailService = emailSender;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return new EmailLogger(_emailService);
    }

    public void Dispose()
    {
    }
}

