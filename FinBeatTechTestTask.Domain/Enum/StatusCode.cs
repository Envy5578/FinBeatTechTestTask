using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeatTechTestTask.Domain.Enum
{
    public enum StatusCode
    {
        InvalidData = 1,
        DataNotFound = 2,
        OK = 200,
        InternalServerError = 500
    }
}
