using Background.Worker.Core.Services.Interfaces;
using Background.Worker.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Services
{
    public class ReminderService : IReminderService
    {
        private readonly ILogger<ReminderService> _logger;
        private readonly ISlackService _slackService;

        public ReminderService(ILogger<ReminderService> logger, ISlackService slackService)
        {
            _logger = logger;
            _slackService = slackService;
        }

        public async Task Start()
        {
            try
            {
                await _slackService.SendMessage("Hello World!");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "ReminderService raise an error at: {time}", DateTimeOffset.Now);
            }
        }
    }
}
