using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Entities
{
    public class BaseEntity
    {
        public DateTime Started { get; set; }
        public DateTime Completed { get; set; }
        public DateTime Created { get; set; }
    }
}
