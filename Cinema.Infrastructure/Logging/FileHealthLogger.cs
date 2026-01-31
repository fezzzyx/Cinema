using System.Text;

namespace Cinema.Infrastructure.Logging;

public class FileHealthLogger
{
    private readonly string _logFilePath;

    public FileHealthLogger()
    {
        var logsDir = Path.Combine(AppContext.BaseDirectory, "health-logs");
        Directory.CreateDirectory(logsDir);

        _logFilePath = Path.Combine(logsDir, "api-health.log");
    }

    public async Task LogAsync(string message)
    {
        var logEntry = new StringBuilder()
            .AppendLine("===================================")
            .AppendLine(message)
            .AppendLine();

        await File.AppendAllTextAsync(_logFilePath, logEntry.ToString());
    }
}