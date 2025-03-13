using Microsoft.Extensions.Logging;
using System.Diagnostics;
using WatchTower.Logging;

namespace WatchTower.Utils;

public static class CommandRunner
{
    public static async Task<string> RunCommandAsync(string command)
    {
        var logger = Logger.LoggerInstance;
        logger.LogDebug($"Executing command: {command}");

        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };

        process.Start();
        var output = await process.StandardOutput.ReadToEndAsync();
        await process.WaitForExitAsync();

        logger.LogDebug($"Command output: {output.Trim()}");
        return output.Trim();
    }
}
