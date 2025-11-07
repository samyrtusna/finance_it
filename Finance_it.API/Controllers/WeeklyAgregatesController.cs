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
    public class WeeklyAgregatesController : BaseController
    {
        private readonly IWeeklyAgregatesService _service;

        public WeeklyAgregatesController(IWeeklyAgregatesService service)
        {
            _service = service;
        }

        
        [HttpGet("CurrentWeekAgregates")]
        public async Task<ActionResult<ApiResponseDto<CurrentWeekAgregateResponseDto>>> GetCurrentWeekAgregates()
        {
            var response = await _service.GetCurrentWeekAgregatesAsync(UserId);

            return Ok(response);
        }

   
        [HttpGet("AllWeeklyAgregatesOfTheYear")]
        public async Task<ActionResult<ApiResponseDto<IQueryable<WeeklyAgregateResponseDto>>>> GetAllWeeklyAgregatesOfTheYear()
        {
            var response = await _service.GetAllWeeklyAgregatesOfTheYearAsync(UserId);

            return Ok(response);
        }

        
        [HttpGet("WeeklyAgregatesByWeekStartDate")]
        public async Task<ActionResult<ApiResponseDto<IQueryable<WeeklyAgregateResponseDto>>>> GetWeeklyAgregatesByWeekStartDate([FromRoute]DateTime weekStartDate)
        {            
            var response = await _service.GetWeeklyAgregatesByWeekStartDateAsync(UserId, weekStartDate);

            return Ok(response);
        }
    }
}
