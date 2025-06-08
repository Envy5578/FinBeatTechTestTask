using FinBeatTechTestTask.Domain.Filters.DataFilter;
using FinBeatTechTestTask.Domain.Response;
using FinBeatTechTestTask.Domain.ViewModels.PairValue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinBeatTechTestTask.Service.Interfaces
{
    public interface IPairValueService
    {
        Task<IBaseResponse<PairValueViewModel>> RefreshPairValues(List<PairValueViewModel> pairValues);

        Task<IBaseResponse<PaginatedResponse<List<PairValueViewModel>>>> GetPairValues(PairValueFilter filter);

    }
}
