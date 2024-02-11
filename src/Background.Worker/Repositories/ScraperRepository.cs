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
    public class ScraperRepository : BaseRepository<ScraperData>, IScraperRepository
    {
        public ScraperRepository(IDistributedCache redisCache) : base(redisCache)
        {
        }
    }
}
