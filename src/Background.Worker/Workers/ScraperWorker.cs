using Background.Worker.Core.Options;
using Background.Worker.Services.Interfaces;
using Background.Worker.Workers.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Workers
{
    public class ScraperWorker : IScraperWorker
    {
        private readonly IReminderService _reminderService;
        private readonly ILogger<ScraperWorker> _logger;
        private readonly WorkerOptions _workerOptions;

        public ScraperWorker(IReminderService reminderService, ILogger<ScraperWorker> logger, IOptions<WorkerOptions> workerOptions)
        {
            _reminderService = reminderService;
            _logger = logger;
            _workerOptions = workerOptions.Value;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning("ScraperWorker running at: {time}", DateTimeOffset.Now);
                await _reminderService.Start();
                _logger.LogWarning("ScraperWorker completed at: {time}", DateTimeOffset.Now);
                await Task.Delay(_workerOptions.ScraperDelayTime, stoppingToken);
            }
        }
    }
}
