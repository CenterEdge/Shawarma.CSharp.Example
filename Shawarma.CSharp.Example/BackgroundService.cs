using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Shawarma.CSharp.Example
{
    public class BackgroundService : IHostedService
    {
        private readonly ILogger<BackgroundService> _logger;
        private CancellationTokenSource _cancellationTokenSource;

        public BackgroundService(ILogger<BackgroundService> logger)
        {
            _logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            var localCts = _cancellationTokenSource = new CancellationTokenSource();

#pragma warning disable 4014
            Task.Run(async () =>
            {
                var i = 0;

                while (!localCts.IsCancellationRequested)
                {
                    _logger.LogInformation($"Background process running, iteration {i++}");

                    await Task.Delay(5000, localCts.Token);
                }
            }, localCts.Token);
#pragma warning restore 4014

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;

            return Task.CompletedTask;
        }
    }
}
