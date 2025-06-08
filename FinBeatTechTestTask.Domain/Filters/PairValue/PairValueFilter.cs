using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeatTechTestTask.Domain.Filters.DataFilter
{
    public class PairValueFilter : PagingFilter
    {
        public int? Id { get; set; }
        public string? Code { get; set; }
        public string? Value { get; set; }
    }
}
