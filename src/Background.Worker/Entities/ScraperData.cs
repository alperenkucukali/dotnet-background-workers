using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Entities
{
    public class ScraperData : BaseEntity
    {
        public string ImageRelativePath { get; set; } = null!;
        public string Name { get; set; } = null!;
    }
}
