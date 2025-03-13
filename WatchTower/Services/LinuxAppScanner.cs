using WatchTower.Models;
using WatchTower.Utils;

namespace WatchTower.Services;

public class LinuxAppScanner : IAppScanner
{
    public async Task<List<InstalledApp>> GetInstalledAppsAsync()
    {
        var output = await CommandRunner.RunCommandAsync("dpkg-query -W -f='${Package} ${Version}\n'");
        var apps = output.Split("\n")
            .Where(line => !string.IsNullOrWhiteSpace(line))
            .Select(line =>
            {
                var parts = line.Split(' ');
                return new InstalledApp
                {
                    Name = parts[0],
                    Version = parts.Length > 1 ? parts[1] : "Unknown"
                };
            })
            .ToList();
        return apps;
    }
}