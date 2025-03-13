using Microsoft.Extensions.Logging;
using WatchTower.Logging;

namespace WatchTower.Services;

public static class AppScannerFactory
{
    public static IAppScanner? CreateScanner()
    {
        var logger = Logger.LoggerInstance;

        if (OperatingSystem.IsWindows())
        {
            logger.LogInformation("Running on Windows.");
            return new WindowsAppScanner();
        }
        if (OperatingSystem.IsLinux())
        {
            logger.LogInformation("Running on Linux.");
            return new LinuxAppScanner();
        }
        if (OperatingSystem.IsMacOS())
        {
            logger.LogInformation("Running on macOS.");
            return new MacAppScanner();
        }

        logger.LogError("Unsupported OS detected.");
        return null;
    }
}