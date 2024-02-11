using Background.Worker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Repositories.Interfaces
{
    public interface IScraperRepository
    {
        Task Add(ScraperData data);
        Task<ScraperData?> Get();
        Task Delete();
    }
}
