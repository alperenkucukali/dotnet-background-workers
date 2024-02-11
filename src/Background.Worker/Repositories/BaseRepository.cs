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
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly IDistributedCache _redisCache;

        public BaseRepository(IDistributedCache redisCache)
        {
            _redisCache = redisCache;
        }

        public async Task Add(BaseData<T> data, TimeSpan? ttl = null)
        {
            data.Created = DateTime.UtcNow;
            if (ttl is null)
                await _redisCache.SetStringAsync(nameof(T), JsonConvert.SerializeObject(data));
            else
            {
                var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(ttl.Value);
                await _redisCache.SetStringAsync(nameof(T), JsonConvert.SerializeObject(data), options);
            }
        }

        public async Task Delete()
        {
            await _redisCache.RemoveAsync(nameof(T));
        }

        public async Task<BaseData<T>?> Get()
        {
            var data = await _redisCache.GetStringAsync(nameof(T));
            if (string.IsNullOrEmpty(data)) return null;
            return JsonConvert.DeserializeObject<BaseData<T>>(data);
        }
    }
}
