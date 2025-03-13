using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using WatchTower.Logging;
using WatchTower.Services;

internal class Program
{
    private static async Task Main(string[] args)
    {
        // Load Configuration
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        // Initialize Logger
        Logger.InitializeLogger(configuration);
        var logger = Logger.LoggerInstance;

        logger.LogInformation("WatchTower: Starting scan for installed applications...");

        var scanner = AppScannerFactory.CreateScanner();
        if (scanner == null)
        {
            logger.LogError("Unsupported OS detected.");
            return;
        }

        bool detailedLogging = configuration.GetValue<bool>("AppScanner:EnableDetailedLogging");

        var apps = await scanner.GetInstalledAppsAsync();

        logger.LogInformation("Scan completed. Listing applications:");
        foreach (var app in apps)
        {
            if (detailedLogging)
                logger.LogInformation($"- {app.Name} (Version: {app.Version})");
            else
                Console.WriteLine($"- {app.Name}");
        }
    }
}