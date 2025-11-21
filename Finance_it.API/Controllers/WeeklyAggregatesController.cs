using System.Security.Claims;
using Finance_it.API.Infrastructure.Exceptions;
using Finance_it.API.Models.Dtos.ApiResponsesDtos;
using Finance_it.API.Models.Dtos.WeeklyAgregateDtos;
using Finance_it.API.Services.WeeklyAgregateServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Finance_it.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class WeeklyAggregatesController : BaseController
    {
        private readonly IWeeklyAggregatesService _service;

        public WeeklyAggregatesController(IWeeklyAggregatesService service)
        {
            _service = service;
        }

        
        [HttpGet("CurrentWeekAgregates")]
        public async Task<ActionResult<ApiResponseDto<CurrentWeekAggregateResponseDto>>> GetCurrentWeekAgregates()
        {
            var response = await _service.GetCurrentWeekAggregatesAsync(UserId);

            return Ok(response);
        }

   
        [HttpGet("AllWeeklyAgregatesOfTheYear")]
        public async Task<ActionResult<ApiResponseDto<IQueryable<WeeklyAggregateResponseDto>>>> GetAllWeeklyAgregatesOfTheYear()
        {
            var response = await _service.GetAllWeeklyAggregatesOfTheYearAsync(UserId);

            return Ok(response);
        }

        
        [HttpGet("WeeklyAgregatesByWeekStartDate")]
        public async Task<ActionResult<ApiResponseDto<IQueryable<WeeklyAggregateResponseDto>>>> GetWeeklyAgregatesByWeekStartDate([FromRoute]DateTime weekStartDate)
        {            
            var response = await _service.GetWeeklyAggregatesByWeekStartDateAsync(UserId, weekStartDate);

            return Ok(response);
        }
    }
}
