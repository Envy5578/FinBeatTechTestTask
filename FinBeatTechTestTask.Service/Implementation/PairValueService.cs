using FinBeatTechTestTask.DAL.Interfaces;
using FinBeatTechTestTask.Domain.Entity;
using FinBeatTechTestTask.Domain.Enum;
using FinBeatTechTestTask.Domain.Filters.DataFilter;
using FinBeatTechTestTask.Domain.Response;
using FinBeatTechTestTask.Domain.ViewModels.PairValue;
using FinBeatTechTestTask.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace FinBeatTechTestTask.Service.Implementation
{
    public class PairValueService : IPairValueService
    {
        private readonly IBaseRepository<PairValueEntity> _pairValueRepository;

        public PairValueService(IBaseRepository<PairValueEntity> pairValueRepository)
        {
            _pairValueRepository = pairValueRepository;
        }

        public async Task<IBaseResponse<PairValueViewModel>> RefreshPairValues(List<PairValueViewModel> pairValues)
        {
            try
            {
                await _pairValueRepository.Clear();

                var newEntities = pairValues
                    .Select(p => new PairValueEntity
                    {
                        Code = p.Code,
                        Value = p.Value
                    })
                    .OrderBy(e => e.Code);

                await _pairValueRepository.AddRange(newEntities);

                return new BaseResponse<PairValueViewModel> 
                {
                    Description = "Data saved",
                    StatusCode = Domain.Enum.StatusCode.OK,
                };
            }
            catch (Exception ex) 
            {
                return new BaseResponse<PairValueViewModel>
                {
                    Description = ex.Message,
                    StatusCode = Domain.Enum.StatusCode.InternalServerError
                };
            }
        }
        public async Task<IBaseResponse<PaginatedResponse<List<PairValueViewModel>>>> GetPairValues(PairValueFilter filter)
        {
            try
            {
                var query = _pairValueRepository.GetAll();

                if(query == null)
                {
                    return new BaseResponse<PaginatedResponse<List<PairValueViewModel>>>
                    {
                        StatusCode = Domain.Enum.StatusCode.DataNotFound
                    };
                }

                if (filter.Id.HasValue)
                    query = query.Where(x => x.Id == filter.Id.Value);

                if (filter.Code.HasValue)
                    query = query.Where(x => x.Code == filter.Code);

                if (!string.IsNullOrWhiteSpace(filter.Value))
                    query = query.Where(x => x.Value.Contains(filter.Value));

                var totalCount = await query.CountAsync();

                var items = await query
                    .OrderBy(x => x.Code)
                    .Skip(filter.Skip)
                    .Take(filter.PageSize)
                    .Select(x => new PairValueViewModel
                    {
                        Id = x.Id,
                        Code = x.Code,
                        Value = x.Value
                    })
                    .ToListAsync();

                

                return new BaseResponse<PaginatedResponse<List<PairValueViewModel>>>
                {
                    StatusCode = StatusCode.OK,
                    Data = new PaginatedResponse<List<PairValueViewModel>>
                    {
                        Items = items,
                        TotalCount = totalCount
                    }
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<PaginatedResponse<List<PairValueViewModel>>>
                {
                    Description = ex.Message,
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
