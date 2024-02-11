using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Entities
{
    public class ReminderData : BaseEntity
    {
        public string Message { get; set; } = null!;
        public string Channel { get; set; } = null!;
    }
}
