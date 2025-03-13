using WatchTower.Models;

namespace WatchTower.Services;

public interface IAppScanner
{
    Task<List<InstalledApp>> GetInstalledAppsAsync();
}
