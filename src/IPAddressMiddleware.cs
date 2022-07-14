namespace CityBreaks;

// Middleware that follows the conventions-based approach is created as a singleton
// when the application first starts up, which means that there is only one instance
// created for the lifetime of the application.
// This instance is reused for every request that reaches it.
public class IPAddressMiddleware
{
    private readonly RequestDelegate _next;

    public IPAddressMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, ILogger<IPAddressMiddleware> logger)
    {
        var ipAddress = context.Connection.RemoteIpAddress;

        logger.LogInformation("Visitor is from {IPAddress}", ipAddress);

        await _next(context);
    }
}

public static class Extensions
{
    // It is recommended that you create your own extension method on IApplicationBuilder
    // to register your middleware.
    public static IApplicationBuilder UseIPAddress(this IApplicationBuilder app)
    {
        return app.UseMiddleware<IPAddressMiddleware>();
    }
}