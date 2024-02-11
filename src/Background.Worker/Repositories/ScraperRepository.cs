using Background.Worker.Entities;
using Background.Worker.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Repositories
{
    public class ScraperRepository : IScraperRepository
    {
        private readonly IDistributedCache _redisCache;
        private const string _key = "Scraper";

        public ScraperRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task Add(ScraperData data)
        {
            data.Created = DateTime.UtcNow;
            await _redisCache.SetStringAsync(_key, JsonConvert.SerializeObject(data));
        }

        public async Task<ScraperData?> Get()
        {
            var scraperData = await _redisCache.GetStringAsync(_key);
            if (string.IsNullOrEmpty(scraperData)) return null;
            return JsonConvert.DeserializeObject<ScraperData?>(scraperData);
        }

        public async Task Delete()
        {
            await _redisCache.RemoveAsync(_key);
        }
    }
}
