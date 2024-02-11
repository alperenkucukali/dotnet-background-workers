using Background.Worker.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Background.Worker.Repositories.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        Task Add(BaseData<T> data, TimeSpan? ttl = null);
        Task<BaseData<T>?> Get();
        Task Delete();
    }
}
