using System.Runtime.CompilerServices;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.YearlyAgregateDtos;
using Finance_it.API.Services.YearlyAgregatesServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class YearlyAggregatesController : BaseController
    {
        private readonly IYearlyAggregatesService _service;

        public YearlyAggregatesController(IYearlyAggregatesService service)
        {
            _service = service;
        }

        [HttpGet("CurrentYearAgregates")]
        public async Task<ActionResult<ApiResponseDto<CurrentYearAggregatesDto>>> GetCurrentYearAgregates()
        {
            var response = await _service.GetCurrentYearAggregatesAsync(UserId);

            return Ok(response);
        }

        [HttpGet("AllYearlyAgregates")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<YearlyAggregateResponseDto>>>> GetAllYearlyAgregates()
        {
            var response = await _service.GetAllYearlyAggregatesAsync(UserId);

            return Ok(response);
        }

        [HttpGet("YearlyAgregatesByYear")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<YearlyAggregateResponseDto>>>> GetYearlyAgregatesByYear(DateTime date)
        {
            var response = await _service.GetYearlyAggregatesByYearAsync(UserId, date);

            return Ok(response);
        }
    }
}
