using Background.Worker.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Core.Options
{
    public class SlackOptions
    {
        public string Uri { get; set; } = null!;
        public string AccessToken { get; set; } = null!;
        public string SigninToken { get; set; } = null!;
        public Dictionary<string, string> Channels { get; set; } = null!;
    }
}
