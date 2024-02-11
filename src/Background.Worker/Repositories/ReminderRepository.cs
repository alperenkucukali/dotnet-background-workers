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
    public class ReminderRepository : BaseRepository<ReminderData>, IReminderRepository
    {
        public ReminderRepository(IDistributedCache redisCache) : base(redisCache)
        {
        }
    }
}
