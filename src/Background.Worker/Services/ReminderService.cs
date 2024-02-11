using Background.Worker.Core.Services.Interfaces;
using Background.Worker.Entities;
using Background.Worker.Repositories.Interfaces;
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
        private readonly IReminderRepository _reminderRepository;

        public ReminderService(ILogger<ReminderService> logger, ISlackService slackService, IReminderRepository reminderRepository)
        {
            _logger = logger;
            _slackService = slackService;
            _reminderRepository = reminderRepository;
        }

        public async Task Start()
        {
            try
            {
                var reminderData = await _reminderRepository.Get();
                if (reminderData is null) reminderData = new();
                var newReminderData = new ReminderData();
                try
                {
                    newReminderData.Started = DateTime.UtcNow;
                    var msg = "Hello World!";
                    newReminderData.Message = msg;
                    newReminderData.Channel = "Default";
                    await _slackService.SendMessage(msg);
                    newReminderData.Completed = DateTime.UtcNow;
                }
                catch (Exception e)
                {
                    newReminderData.Completed = DateTime.UtcNow;
                    _logger.LogError(e, "ReminderService raise an error at: {time}", DateTimeOffset.Now);
                }
                reminderData.Data.Add(newReminderData);
                await _reminderRepository.Add(reminderData);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "ReminderService raise an critical error at: {time}", DateTimeOffset.Now);
            }

        }
    }
}
