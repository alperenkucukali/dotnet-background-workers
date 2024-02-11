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
    public class ReminderRepository : IReminderRepository
    {
        private readonly IDistributedCache _redisCache;
        private const string _key = "Reminder";

        public ReminderRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task Add(ReminderData data)
        {
            data.Created = DateTime.UtcNow;
            var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(12));
            await _redisCache.SetStringAsync(_key, JsonConvert.SerializeObject(data), options);
        }

        public async Task<ReminderData?> Get()
        {
            var reminderData = await _redisCache.GetStringAsync(_key);
            if (string.IsNullOrEmpty(reminderData)) return null;
            return JsonConvert.DeserializeObject<ReminderData?>(reminderData);
        }

        public async Task Delete()
        {
            await _redisCache.RemoveAsync(_key);
        }
    }
}
