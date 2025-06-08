using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeatTechTestTask.DAL.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task AddRange(IEnumerable<T> entities);
        IQueryable<T> GetAll();
        Task Clear();
    }
}
