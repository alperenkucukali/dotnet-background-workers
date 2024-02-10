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
    public class ReminderWorker : IReminderWorker
    {
        private readonly IReminderService _reminderService;
        private readonly ILogger<ReminderWorker> _logger;
        private readonly WorkerOptions _workerOptions;

        public ReminderWorker(IReminderService reminderService, ILogger<ReminderWorker> logger, IOptions<WorkerOptions> workerOptions)
        {
            _reminderService = reminderService;
            _logger = logger;
            _workerOptions = workerOptions.Value;
        }

        public async Task DoWorkAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogWarning("ReminderWorker running at: {time}", DateTimeOffset.Now);
                await _reminderService.Start();
                _logger.LogWarning("ReminderWorker completed at: {time}", DateTimeOffset.Now);
                await Task.Delay(_workerOptions.ReminderDelayTime, stoppingToken);
            }
        }
    }
}
