using FinBeatTechTestTask.Domain.Filters.DataFilter;
using FinBeatTechTestTask.Domain.ViewModels.PairValue;
using FinBeatTechTestTask.Service.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinBeatTechTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PairValueController : Controller
    {
        private readonly IPairValueService _pairValueService;

        public PairValueController(IPairValueService pairValueService)
        {
            _pairValueService = pairValueService;
        }

        /// <summary>
        /// Updates pair values with the provided list
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /pairValues/refresh
        /// [
        ///     {
        ///         "key": 101,
        ///         "value": "Some value A"
        ///     },
        ///     {
        ///         "key": 202,
        ///         "value": "Some value B"
        ///     }
        /// ]
        /// </remarks>
        /// <param name="model">List of PairValueViewModel with integer keys and string values</param>
        /// <returns>Returns a description of the result of the operation</returns>
        /// <response code="200">Update successful</response>
        /// <response code="400">Invalid input or operation failed</response>
        [HttpPost]
        public async Task<IActionResult> RefreshPairValues(List<PairValueViewModel> model)
        {
            var result = await _pairValueService.RefreshPairValues(model);

            if (result.StatusCode == Domain.Enum.StatusCode.OK) 
            { 
                return Ok(new {data = result.Description});            
            }

            return BadRequest(new {data = result.Description});
        }

        /// <summary>
        /// Retrieves a filtered and paginated list of pair values
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /pairValues?search=abc&pageSize=10&skip=0
        /// 
        /// Sample response:
        /// {
        ///     "data": [
        ///         {
        ///             "id": 1,
        ///             "key": 101,
        ///             "value": "Some value A"
        ///         },
        ///         {
        ///             "id": 2,
        ///             "key": 202,
        ///             "value": "Some value B"
        ///         }
        ///     ],
        ///     "total": 2,
        ///     "pageSize": 10,
        ///     "skip": 0
        /// }
        /// </remarks>
        /// <param name="filter">Filter parameters for searching and pagination</param>
        /// <returns>Returns a paginated list of PairValueViewModel objects</returns>
        /// <response code="200">Successful retrieval</response>
        /// <response code="404">No data found</response>
        /// <response code="400">Invalid filter parameters</response>
        [HttpGet]
        public async Task<IActionResult> GetPairValues([FromQuery] PairValueFilter filter)
        {
            var result = await _pairValueService.GetPairValues(filter);

            if (result.StatusCode == Domain.Enum.StatusCode.OK)
            {
                return Ok(new
                {
                    data = result.Data.Items,
                    total = result.Data.TotalCount,
                    pageSize = filter.PageSize,
                    skip = filter.Skip
                });
            }
            else if (result.StatusCode == Domain.Enum.StatusCode.DataNotFound)
            {
                return NotFound();
            }
            return BadRequest(new { data = result.Data });
        }
    }
}
