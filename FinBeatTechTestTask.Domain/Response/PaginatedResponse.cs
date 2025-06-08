using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeatTechTestTask.Domain.Response
{
    public class PaginatedResponse<T>
    {
        public T Items { get; set; } = default!;
        public int TotalCount { get; set; }
    }
}
