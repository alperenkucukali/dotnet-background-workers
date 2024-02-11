using Background.Worker.Core.Services;
using Background.Worker.Core.Services.Interfaces;
using Background.Worker.Entities;
using Background.Worker.Repositories;
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
    public class ScraperService : IScraperService
    {
        private readonly ILogger<ScraperService> _logger;
        private readonly IImageService _imageService;
        private readonly IScraperRepository _scraperRepository;

        public ScraperService(ILogger<ScraperService> logger, IImageService imageService, IScraperRepository scraperRepository)
        {
            _logger = logger;
            _imageService = imageService;
            _scraperRepository = scraperRepository;
        }

        public async Task Start()
        {
            try
            {
                var scraperData = await _scraperRepository.Get();
                scraperData ??= new();
                var newScraperData = new ScraperData();
                try
                {
                    newScraperData.Started = DateTime.UtcNow;
                    //await _imageService.Upload();
                    newScraperData.Completed = DateTime.UtcNow;
                }
                catch (Exception e)
                {
                    newScraperData.Completed = DateTime.UtcNow;
                    _logger.LogError(e, "ScraperService raise an error at: {time}", DateTimeOffset.Now);
                }
                scraperData.Data.Add(newScraperData);
                await _scraperRepository.Add(scraperData);
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "ScraperService raise an critical error at: {time}", DateTimeOffset.Now);
            }

        }
    }
}
