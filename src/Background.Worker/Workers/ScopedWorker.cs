using Background.Worker.Workers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Workers
{
    public class ScopedWorker : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<ScopedWorker> _logger;

        public ScopedWorker(IServiceProvider serviceProvider, ILogger<ScopedWorker> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ScopedWorker)} is running.");

            await DoWorkAsync(stoppingToken);
        }

        private async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ScopedWorker)} is working.");
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                var scopedProcessingServices = scope.ServiceProvider.GetServices<IScopedProcessingService>();
                await Task.WhenAll(scopedProcessingServices.Select(x => x.DoWorkAsync(stoppingToken)));
            }
        }

        public override async Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{nameof(ScopedWorker)} is stopping.");

            await base.StopAsync(stoppingToken);
        }
    }
}
