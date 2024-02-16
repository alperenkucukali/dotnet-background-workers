using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Entities
{
    public class BaseData<T> where T : BaseEntity
    {
        public BaseData()
        {
            Data = new List<T>();
        }
        public DateTime Created { get; set; }
        public List<T> Data { get; set; } = null!;
    }
}
