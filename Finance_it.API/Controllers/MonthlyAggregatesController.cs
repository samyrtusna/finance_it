using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.MonthlyAgregateDtos;
using Finance_it.API.Services.MonthlyAgregateServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MonthlyAggregatesController : BaseController
    {
        private readonly IMonthlyAggregatesService _service;

        public MonthlyAggregatesController(IMonthlyAggregatesService service)
        {
            _service = service;
        }

        [HttpGet("CurrentMonthAgregates")]
        public async Task<ActionResult<ApiResponseDto<CurrentMonthAggregatesDto>>> GetCurrentMonthAgregates() 
        {
            var response = await _service.GetCurrentMonthAggregatesAsync(UserId);

            return Ok(response);
        }

        [HttpGet("AllMonthlyAgregatesOfTheYear")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<MonthlyAggregateResponseDto>>>> GetAllMonthlyAgregatesOfTheYear(DateTime date)
        {
            var response = await _service.GetAllMonthlyAggregatesOfTheYearAsync(UserId, date);

            return Ok(response);
        }

        [HttpGet("MonthlyAgregatesByMonth")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<MonthlyAggregateResponseDto>>>> GetMonthlyAgregatesByMonth(DateTime date)
        {
            var response = await _service.GetMonthlyAggregatesByMonthAsync(UserId, date);

            return Ok(response);
        }
    }
}
