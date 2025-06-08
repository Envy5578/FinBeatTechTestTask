using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeatTechTestTask.Domain.Filters
{
    public class PagingFilter
    {
        public int PageSize { get; set; } = 10;
        public int Skip { get; set; } = 0;
    }
}
