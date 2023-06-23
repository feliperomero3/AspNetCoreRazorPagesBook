using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

namespace CityBreaks.Logging;

public class LoggingConfiguration
{
    public static Serilog.ILogger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Information()
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.AspNetCore.Identity", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authorization", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Server.Kestrel.Core", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing.Matching", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
            .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level}] {SourceContext} {Message:jl}{NewLine}{Exception}")
            .WriteTo.File(path: "Logs\\log.json", formatter: new JsonFormatter(), rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}
