using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Shawarma.AspNetCore.Hosting;

namespace Shawarma.CSharp.Example
{
    public class BackgroundService : GenericShawarmaService
    {
        private readonly ILogger<BackgroundService> _logger;
        private CancellationTokenSource _cancellationTokenSource;

        public BackgroundService(ILogger<BackgroundService> logger) : base(logger)
        {
            _logger = logger;
        }

        protected override Task StartInternalAsync(CancellationToken cancellationToken)
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

        protected override Task StopInternalAsync(CancellationToken cancellationToken)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = null;

            return Task.CompletedTask;
        }
    }
}
