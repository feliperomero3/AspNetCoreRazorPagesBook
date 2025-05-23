﻿using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;
using ILogger = Serilog.ILogger;

namespace CityBreaks.Logging;

public static class LoggingConfiguration
{
    public static ILogger CreateLogger()
    {
        return new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("CityBreaks", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Hosting", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Identity", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authorization", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Server.Kestrel.Core", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.AspNetCore.Routing.Matching", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.Mvc", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft.AspNetCore.StaticFiles", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.EntityFrameworkCore.Database.Command", LogEventLevel.Information)
            .WriteTo.Console(outputTemplate: "{Timestamp:HH:mm:ss.fff} [{Level}] {SourceContext} {Message:jl}{NewLine}{Exception}")
            .WriteTo.File(path: "Logs\\log.json", formatter: new JsonFormatter(), rollingInterval: RollingInterval.Day)
            .CreateLogger();
    }
}
