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
    public class MonthlyAgregatesController : BaseController
    {
        private readonly IMonthlyAgregatesService _service;

        public MonthlyAgregatesController(IMonthlyAgregatesService service)
        {
            _service = service;
        }

        [HttpGet("CurrentMonthAgregates")]
        public async Task<ActionResult<ApiResponseDto<CurrentMonthAgregatesDto>>> GetCurrentMonthAgregates() 
        {
            var response = await _service.GetCurrentMonthAgregatesAsync(UserId);

            return Ok(response);
        }

        [HttpGet("AllMonthlyAgregatesOfTheYear")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<MonthlyAgregateResponseDto>>>> GetAllMonthlyAgregatesOfTheYear(DateTime date)
        {
            var response = await _service.GetAllMonthlyAgregatesOfTheYearAsync(UserId, date);

            return Ok(response);
        }

        [HttpGet("MonthlyAgregatesByMonth")]
        public async Task<ActionResult<ApiResponseDto<IEnumerable<MonthlyAgregateResponseDto>>>> GetMonthlyAgregatesByMonth(DateTime date)
        {
            var response = await _service.GetMonthlyAgregatesByMonthAsync(UserId, date);

            return Ok(response);
        }
    }
}
