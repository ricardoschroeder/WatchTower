using System.Management;
using System.Runtime.Versioning;
using WatchTower.Models;

namespace WatchTower.Services;

[SupportedOSPlatform("Windows")]
public class WindowsAppScanner : IAppScanner
{
    public async Task<List<InstalledApp>> GetInstalledAppsAsync()
    {
        return await Task.Run(() =>
        {
            var apps = new List<InstalledApp>();
            using var searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Product");
            foreach (ManagementObject obj in searcher.Get())
            {
                apps.Add(new InstalledApp
                {
                    Name = obj["Name"]?.ToString() ?? "Unknown",
                    Version = obj["Version"]?.ToString() ?? "Unknown"
                });
            }
            return apps;
        });
    }
}
