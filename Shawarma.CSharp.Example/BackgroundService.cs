using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shawarma.AspNetCore.Hosting;

namespace Shawarma.CSharp.Example;

public class BackgroundService(ILogger<BackgroundService> logger) : GenericShawarmaService(logger)
{
    public static int IterationCount { get; private set; }

    private CancellationTokenSource _cancellationTokenSource;

    protected override Task StartInternalAsync(CancellationToken cancellationToken)
    {
        var localCts = _cancellationTokenSource = new CancellationTokenSource();

        var restoreFlow = false;
        try
        {
            if (!ExecutionContext.IsFlowSuppressed())
            {
                // Ensure ExecutionContext doesn't flow into a long running background task
                ExecutionContext.SuppressFlow();
                restoreFlow = true;
            }

            // Start the background work, but don't await completion
            _ = DoWorkAsync(localCts.Token);
        }
        finally
        {
            if (restoreFlow)
            {
                ExecutionContext.RestoreFlow();
            }
        }

        return Task.CompletedTask;
    }

    protected override Task StopInternalAsync(CancellationToken cancellationToken)
    {
        _cancellationTokenSource?.Cancel();
        _cancellationTokenSource = null;

        return Task.CompletedTask;
    }

    private async Task DoWorkAsync(CancellationToken cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            try
            {
                logger.LogInformation($"Background process running, iteration {IterationCount++}");
            }
            catch (OperationCanceledException)
            {
                // Ignore, shutting down
                return;
            }
            catch
            {
                // Keep running even after exceptions, but this error case should be logged
            }

            await Task.Delay(5000, cancellationToken);
        }
    }
}
