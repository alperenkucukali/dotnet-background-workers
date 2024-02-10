using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Core.Services.Interfaces
{
    public interface ISlackService
    {
        Task SendMessage(string message, string channelKey = "Default");
    }
}
