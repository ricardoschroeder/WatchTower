using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WatchTower.Logging;

public static class Logger
{
    public static ILoggerFactory LoggerFactory { get; private set; } = null!;
    public static ILogger LoggerInstance { get; private set; } = null!;

    public static void InitializeLogger(IConfiguration configuration)
    {
        LoggerFactory = Microsoft.Extensions.Logging.LoggerFactory.Create(builder =>
        {
            builder.AddConsole();
            var logLevel = configuration.GetValue<string>("Logging:LogLevel");
            builder.SetMinimumLevel(logLevel switch
            {
                "Debug" => LogLevel.Debug,
                "Warning" => LogLevel.Warning,
                "Error" => LogLevel.Error,
                _ => LogLevel.Information
            });
        });

        LoggerInstance = LoggerFactory.CreateLogger("WatchTower");
    }
}