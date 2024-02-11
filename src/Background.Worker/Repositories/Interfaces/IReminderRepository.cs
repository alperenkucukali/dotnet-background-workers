using Background.Worker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Repositories.Interfaces
{
    public interface IReminderRepository
    {
        Task Add(ReminderData data);
        Task<ReminderData?> Get();
        Task Delete();
    }
}
