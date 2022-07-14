namespace CityBreaks;

// The recommended approach to writing new middleware classes
// involves implementing the IMiddleware interface.
public class IPAddressMiddleware : IMiddleware
{
    private readonly ILogger<IPAddressMiddleware> _logger;

    public IPAddressMiddleware(ILogger<IPAddressMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var ipAddress = context.Connection.RemoteIpAddress;

        _logger.LogInformation("Visitor is from {IPAddress}", ipAddress);

        await next(context);
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