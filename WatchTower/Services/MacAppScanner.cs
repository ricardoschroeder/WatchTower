using WatchTower.Models;
using WatchTower.Utils;

namespace WatchTower.Services;

public class MacAppScanner : IAppScanner
{
    public async Task<List<InstalledApp>> GetInstalledAppsAsync()
    {
        var output = await CommandRunner.RunCommandAsync("system_profiler SPApplicationsDataType");
        var apps = output.Split("\n")
            .Where(line => line.Contains(": "))
            .Select(line =>
            {
                var parts = line.Split(": ");
                return new InstalledApp
                {
                    Name = parts[0].Trim(),
                    Version = parts.Length > 1 ? parts[1].Trim() : "Unknown"
                };
            })
            .ToList();
        return apps;
    }
}