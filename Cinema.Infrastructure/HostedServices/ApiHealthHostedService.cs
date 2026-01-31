using System.Diagnostics;
using Cinema.Infrastructure.Data;
using Cinema.Infrastructure.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cinema.Infrastructure.HostedServices;

public class ApiHealthHostedService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly FileHealthLogger _logger;

    public ApiHealthHostedService(
        IServiceScopeFactory scopeFactory,
        FileHealthLogger logger)
    {
        _scopeFactory = scopeFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            await CheckHealthAsync();
            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }

    private async Task CheckHealthAsync()
    {
        var timestamp = DateTime.UtcNow;
        string dbStatus;

        try
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            await dbContext.Database.ExecuteSqlRawAsync("SELECT 1");
            dbStatus = "OK";
        }
        catch (Exception ex)
        {
            dbStatus = $"ERROR: {ex.Message}";
        }

        var memory = GC.GetTotalMemory(false) / 1024 / 1024;
        var threadCount = Process.GetCurrentProcess().Threads.Count;

        var log = $"""
                   Time: {timestamp}
                   API Status: Alive
                   Database: {dbStatus}
                   Memory Used: {memory} MB
                   Threads: {threadCount}
                   """;

        await _logger.LogAsync(log);
    }
}