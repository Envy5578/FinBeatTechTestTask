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
        /// Получение Json массива объектов
        /// </summary>
        /// <param name="model">PairValueViewModel object</param>
        /// <returns>Return Request</returns>
        [HttpPost]
        public async Task<IActionResult> RefreshPairValues(List<PairValueViewModel> model)
        {
            var result = await _pairValueService.RefreshPairValues(model);

            if (result.StatusCode == Domain.Enum.StatusCode.OK) 
            { 
                return Ok(new {data = result.Data});            
            }

            return BadRequest(new {data = result.Description});
        }

        /// <summary>
        /// Отправка сортированных данных
        /// </summary>
        /// <param>PairValueFilter object</param>
        /// <returns>Return PairValueViewModel</returns>
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
